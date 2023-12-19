using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Reknighted.Model;
using Reknighted.View;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Reknighted.Model.Items;
using Reknighted.Model.Entities;
using Reknighted.Controller.Collections;

namespace Reknighted.Controller
{

    public static class IPaddr
    {
        public static IPAddress IP = IPAddress.Parse("127.0.0.1");
        public static int Port = 12345; 
    }

    
    // Статический класс, служащий связующим звеном между всеми компонентами игры.
    public static class Game
    {
        #region Поля и свойства

        public static Application app = Application.Current;

        #region Контейнеры интерактивных элементов

        #region Представления предметов

        // Предмет, ждущий подтверждения для купли/продажи
        public static ItemView? _productItem { get; set; }

        // Выбранный (перетаскиваемый мышью) предмет
        private static ItemView? _selectedItem = null;
        public static ItemView? SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        // Список всех предметов на экране
        private static List<ItemView> _allItemViews = new List<ItemView>();
        public static List<ItemView> AllItemsViews { get { return _allItemViews; } }

        // Список предметов в инвентаре торговца
        public static List<ItemView> TraderItems = new();

        #endregion

        #region Представления клеток

        // Клетка, на которую наведён курсор
        private static Cell? _selectedCell = null;

        // Список клеток неактивного инвентаря игрока
        private static List<Cell> _inventoryCells = new List<Cell>();
        public static List<Cell> InventoryCells { get { return _inventoryCells; } }

        // Список клеток активного инвентаря игрока
        private static List<Cell> _equipmentCells = new List<Cell>();
        public static List<Cell> EquipmentCells { get { return _equipmentCells; } }

        // Список клеток инвентаря торговца
        private static List<Cell> _traderCells = new List<Cell>();
        public static List<Cell> TraderCells { get { return _traderCells; } }

        // Список всех клеток на экране
        private static List<Cell> _allCells = new List<Cell>();
        public static List<Cell> AllCells
        {
            get { return _allCells; }
        }

        #endregion

        #endregion

        #region Персонажи

        // Списки всех торговцев и противников
        public static List<TraderModel> AllTraders { get; set; }
        public static List<Fighter> AllFighters { get; set; }

        // Представление игрока

        private static PlayerView? _playerView = null;
        public static PlayerView? PlayerView { get => _playerView; set => _playerView = value; }

        // Модель игрока
        private static PlayerModel? _playerModel = null;
        public static PlayerModel? PlayerModel { get => _playerModel; set => _playerModel = value; }

        // Модель торговца
        private static TraderModel? _currentTrader = null;
        public static TraderModel? CurrentTrader
        {
            get => _currentTrader;
            set
            {
                _currentTrader = value;
                var gw = _window as GameWindow;
                if (gw != null)
                {
                    if (value != null)
                    {
                        gw.gameTabs.SelectedIndex = 0;
                        //gw.knightTabButton.Header = "Торговля";

                        gw.traderView.Model = _currentTrader;
                        gw.traderView.UpdateContent();

                        gw.Resize();
                    }
                    else
                    {
                        gw.gameTabs.SelectedIndex = gw.gameTabs.SelectedIndex;
                        //gw.traderView.UpdateContent();

                        foreach (var item in AllItemsViews)
                        {
                            if (item != null)
                            {
                                if (!item.Model.IsPossessed)
                                {
                                    Grid? grid = (Grid?)item.Parent;
                                    if (grid != null)
                                    {
                                        grid.Children.Remove(item);
                                    }


                                }
                            }

                        }

                        foreach (var cell in TraderCells)
                        {
                            cell.ContentItem = null;
                        }

                        gw.Resize();
                    }



                }

            }
        }

        #endregion

        #region Прочее

        // Главное окно игры
        private static GameWindow? _window = null;
        public static GameWindow? Window
        {
            get { return _window; }
            set
            {
                _window = value;
                var gw = _window as GameWindow;
                PlayerView = gw!.playerView;
                LocationView = gw!.location;
                DialogLib.AwaitingMessage.foreignLabel = _window.locationInfoLabel;
            
            }
        }
        // Карта локации
        public static LocationView? LocationView { get; set; }

        // Коэффициент масштабирования
        public static double Scale = -1;
        public static void CalculateCoeff()
        {
            if (Scale == -1)
            {
                if (_allCells[0] != null && _allCells[1] != null)
                {
                    Point point_0 = _allCells[0].PointToScreen(new Point(0, 0));
                    Point point_1 = _allCells[1].PointToScreen(new Point(0, 0));
                    double d = Math.Abs(point_0.X - point_1.X);

                    Scale = d / 45;
                }
            }
        }

        // Цена путешествия
        private const int MIN_JORNEY_CONST = 15;
        private const int MAX_JORNEY_CONST = 75;
        //public static int NextJorneyCost { get; private set; } = (new Random()).Next(MIN_JORNEY_CONST, MAX_JORNEY_CONST);
        public static Dictionary<Location, int> NextJorneyCosts = new Dictionary<Location, int>() { { Location.ShowRoom, 0 }, { Location.Hearts, 0 }, { Location.Clubs, 0 }, { Location.Spades, 0 }, { Location.Diamonds, 0 } };
        public static void GenerateJorneyCost()
        {
            NextJorneyCosts[Location.Hearts] = (new Random()).Next(MIN_JORNEY_CONST, MAX_JORNEY_CONST);
            NextJorneyCosts[Location.Clubs] = (new Random()).Next(MIN_JORNEY_CONST, MAX_JORNEY_CONST);
            NextJorneyCosts[Location.Spades] = (new Random()).Next(MIN_JORNEY_CONST, MAX_JORNEY_CONST);
            NextJorneyCosts[Location.Diamonds] = (new Random()).Next(MIN_JORNEY_CONST, MAX_JORNEY_CONST);

            //NextJorneyCost = (new Random()).Next(MIN_JORNEY_CONST, MAX_JORNEY_CONST);
        }

        #endregion

        #endregion

        #region Методы

        #region Обновление информации на экране

        private static void Reset()
        {
            foreach (var item in AllItemsViews)
            {
                Grid? grid = (Grid?)item.Parent;
                if (grid != null)
                {
                    grid.Children.Remove(item);
                }
            }
            AllItemsViews.Clear();

            foreach (var cell in AllCells)
            {
                cell.ContentItem = null;
            }

        }
        public static void Update()
        {
            if (Window == null)
            {
                return;
            }

            if (PlayerView != null)
            {
                PlayerView.UpdateStats();
                
            }

            if (Window != null)
            {
                Window.location.Update();
            }

            try
            {
                Reset();

                Window.playerView.UpdateContent();
                if (CurrentTrader != null)
                {
                    Window.traderView.UpdateContent();
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public static void Error(string message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Message(string message, MessageType messageType = MessageType.Information)
        {
            if (Window == null)
            {
                return;
            }


            //if (message == "")
            //{
            //    Window.quitButton.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    Window.quitButton.Visibility = Visibility.Collapsed;
            //}

            Window.locationInfoLabel.Foreground = new SolidColorBrush(Colors.Gray);

            if (messageType == MessageType.Win)
            {
                Window.locationInfoLabel.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (messageType == MessageType.Loose)
            {
                Window.locationInfoLabel.Foreground = new SolidColorBrush(Colors.Red);
            }

            Window.locationInfoLabel.Content = message;
        }

        #endregion

        #region Обработчики движения мыши

        // Положения курсора мыши
        public static Point? MousePreviousPosition = null;
        public static Point? MouseCurrentPosition = null;

        public static void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            MousePreviousPosition = MouseCurrentPosition;
            MouseCurrentPosition = e.GetPosition(_window);

            GameWindow? gameWindow = _window;
            if (gameWindow != null)
            {
                if (gameWindow.gameTabs.SelectedIndex == 0)
                {
                    List<Cell> cells = new List<Cell>();

                    foreach (Cell cell in AllCells)
                    {
                        cell.IsPointed = false;

                        var mousePos = _window.PointToScreen(Mouse.GetPosition(_window));
                        bool isOver = true;

                        Point cellPos = cell.PointToScreen(new Point(0, 0));
                        isOver &= mousePos.X >= cellPos.X;
                        isOver &= mousePos.X <= cellPos.X + cell.Width;
                        isOver &= mousePos.Y >= cellPos.Y;
                        isOver &= mousePos.X <= cellPos.X + cell.Width;

                        if (isOver)
                        {
                            cells.Add(cell);
                        }
                    }

                    if (cells.Count > 0)
                    {
                        _selectedCell = cells[cells.Count - 1];
                        _selectedCell.IsPointed = true;
                    }

                    // Центрирование предмета при перетаскивании
                    if (SelectedItem != null && SelectedItem.Model.IsPossessed)
                    {
                        // Да это костыль но по другому я не знаю как
                        SelectedItem.Position = new Point(
                            MouseCurrentPosition.Value.X - SelectedItem.ActualWidth / 1.7,
                            MouseCurrentPosition.Value.Y - SelectedItem.ActualHeight * 1.15
                        );

                        /*
                        Уберите коммент если хотите чтобы иконка оставалась на том же месте 
                        где её и взяли и закоментите другое
                        Point offset = new Point(MouseCurrentPosition.Value.X - MousePreviousPosition.Value.X,
                                             MouseCurrentPosition.Value.Y - MousePreviousPosition.Value.Y);
                        Item.Position = new Point(Item.Position.X + offset.X, Item.Position.Y + offset.Y);
                         */
                    }
                }
            }
        }

        public static void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            GameWindow? gameWindow = _window;
            if (gameWindow != null)
            {
                if (gameWindow.gameTabs.SelectedIndex == 0)
                {
                    if (SelectedItem != null)
                    {
                        if (_selectedCell != null)
                        {
                            SelectedItem.Model.MoveToCell(_selectedCell);
                        }

                        Update();

                        SelectedItem.Opacity = 1;
                        SelectedItem = null;
                    }
                }
            }

        }

        #endregion

        #region Игровой процесс
        public static IFightable? Fight(IFightable? firstFighter, IFightable? secondFighter, int bet, bool noChange = true)
        {
            if (firstFighter == null || secondFighter == null)
            {
                throw new NullReferenceException();
            }

            Random random = new Random();

            if (firstFighter.HealthPercentage <= 0.2 || secondFighter.HealthPercentage <= 0.2)
            {
                MessageBox.Show((string)app.FindResource("lowHealthMessage"));
                return null;
            }

            int margin = (int)(100 * ServerFight(new double[] { firstFighter.Damage, firstFighter.Protection, firstFighter.HealthPercentage }, new double[] { secondFighter.Damage, secondFighter.Protection, secondFighter.HealthPercentage }));
            int result = random.Next(0, 100);

            //MessageBox.Show(result + ", " + margin);

            IFightable? winner = result <= margin ? firstFighter : secondFighter;
            IFightable? looser = result <= margin ? secondFighter : firstFighter;

            if (looser?.ItemReward != null && winner.GetType() == typeof(PlayerModel))
            {
                var rewards = looser.ItemReward.Select(item => (ItemModel)item.Copy()).ToList();
                PlayerModel player = (PlayerModel)winner;
                foreach ( var reward in rewards )
                {
                    player.AddItem(reward);
                }
                
            }

            if (winner != null && (winner != secondFighter && noChange))
            {
                winner.CurrentHealth -= looser.Damage / 4;
                if (winner.Weapon != null) winner.Weapon.CurrentDurability -= looser.Damage;
                if (winner.Armor != null) winner.Armor.CurrentDurability -= looser.Damage;
                winner.Balance += bet;
            }

            if (looser != null && (looser != secondFighter && noChange))
            {
                looser.CurrentHealth -= winner.Damage / 2;
                if (looser.Weapon != null) looser.Weapon.CurrentDurability -= looser.Damage;
                if (looser.Armor != null) looser.Armor.CurrentDurability -= looser.Damage;
                looser.Balance -= bet;
            }

            if (looser is Fighter)
            {
                Game.AllFighters.Remove((Fighter)looser);
                Game.LocationView.RemoveEntity((IMappable)looser);
            }

            return winner;
        }

        #region Обработка сервера
        public static double ServerFight(double[] first, double[] second)
        {
            try
            {
                

                TcpClient client = new TcpClient();

                client.ReceiveTimeout = 1000; // сколько ждём ответа от сервера

                client.Connect(IPaddr.IP, IPaddr.Port); // подключаемся

                NetworkStream stream = client.GetStream(); 

                // конвертируем в байты и отправляем данные
                string sendData = $"{string.Join("_", first)};{string.Join("_", second)}";
                byte[] data = Encoding.ASCII.GetBytes(sendData);
                stream.Write(data, 0, data.Length);

                // получаем ответ
                data = new byte[8];
                int bytesRead = stream.Read(data, 0, data.Length);
                double responseData = BitConverter.ToDouble(data, 0);
                client.Close();
                return responseData;
            }
            catch (Exception ex)
            {
                MessageBox.Show((string)app.FindResource("serverError"), "Error", MessageBoxButton.OK);
                return 1;
            }
        }
        #endregion

        public static void FightWith(Fighter fighter)
        {   
            if (Window == null)
            {
                return;
            }

            Window.gameTabs.SelectedIndex = 0;
            EnemyWindow enemyWindow = new EnemyWindow(fighter);
            enemyWindow.ShowDialog();

            if (!enemyWindow.Success)
            {
                return;
            }

            //ShowLoading("Идёт бой");
            DialogLib.AwaitingMessage.foreignLabel = Game.Window.locationInfoLabel;
            DialogLib.AwaitingMessage.ShowAwaitingMessage(Window.grid, $"{app.FindResource("ongoingBattle")}");
            
            IFightable? winner = Game.Fight(PlayerModel, fighter, enemyWindow.Bet);
            string message = winner == PlayerModel ? $"{app.FindResource("victoryMessage")}" : $"{app.FindResource("defeatMessage")}";
            MessageType messageType = winner == PlayerModel ? MessageType.Win : MessageType.Loose;

            Message(message, messageType);

            Game.PlayerView.UpdateStats();
            Game.Update();
        }


        #endregion

        public static string PathTo(string name)
        {
            try
            {
                return Directory.GetFiles("Images", $"{name}.png", SearchOption.AllDirectories)[0];
            }
            catch
            {
                MessageBox.Show("Файл не найден. " + name);
                throw;
            }
        }

        public static Dictionary<Location, string> LocationString = new() { { Location.Hearts, $"{Game.app.FindResource("btnHearts")}" }, { Location.Clubs, $"{Game.app.FindResource("btnClubs")}" }, { Location.Diamonds, $"{Game.app.FindResource("btnDiamonds")}" }, { Location.Spades, $"{Game.app.FindResource("btnSpades")}" }, { Location.ShowRoom, $"{Game.app.FindResource("nShowroom")}" } };

        public static void InitEntities()
        {
            AllTraders = Entities.AllTradersDefaultState.Values.ToList();
            AllFighters = Entities.AllFightersDefaultState.Values.ToList();
        }

        #endregion
    }
}
