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

namespace Reknighted.Controller
{
    // Статический класс, служащий связующим звеном между всеми компонентами игры.
    public class Game
    {
        #region Поля и свойства

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
                        gw.knightTabButton.Header = "Торговля";

                        gw.traderView.Model = _currentTrader;
                        gw.traderView.UpdateContent();

                        gw.Resize();
                    }
                    else
                    {
                        gw.knightTabButton.Header = "Рыцарь";
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static void Message(string message, MessageType messageType = MessageType.Information)
        {
            if (Window == null)
            {
                return;
            }

            Window.locationInfoLabel.Foreground = new SolidColorBrush(Colors.Gray);

            if (messageType == MessageType.Win)
            {
                Window.locationInfoLabel.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (messageType == MessageType.Loose)
            {
                Window.locationInfoLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            if (messageType == MessageType.Error)
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

            int margin = (int)(100 * Fighting.Fight(new double[] { firstFighter.Damage, firstFighter.Protection, firstFighter.HealthPercentage }, new double[] { secondFighter.Damage, secondFighter.Protection, secondFighter.HealthPercentage }));
            int result = random.Next(0, 100);

            //MessageBox.Show(result + ", " + margin);

            IFightable? winner = result <= margin ? firstFighter : secondFighter;
            IFightable? looser = result <= margin ? secondFighter : firstFighter;

            if (looser?.Reward != null && winner.GetType() == typeof(PlayerModel))
            {
                var reward = looser.Reward.Copy();
                PlayerModel player = (PlayerModel)winner;
                player.AddItem(reward);
            }

            if (winner != null && (winner != secondFighter && noChange))
            {
                winner.CurrentHealth -= looser.Damage / 2;
                if (winner.Weapon != null) winner.Weapon.CurrentDurability -= looser.Damage;
                if (winner.Armor != null) winner.Armor.CurrentDurability -= looser.Damage;
                winner.Balance += bet;
            }

            if (looser != null && (looser != secondFighter && noChange))
            {
                looser.CurrentHealth -= winner.Damage;
                if (looser.Weapon != null) looser.Weapon.CurrentDurability -= looser.Damage;
                if (looser.Armor != null) looser.Armor.CurrentDurability -= looser.Damage;
                looser.Balance -= bet;
            }

            return winner;
        }

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

            IFightable? winner = Game.Fight(PlayerModel, fighter, enemyWindow.Bet);
            string message = winner == PlayerModel ? "Победа!" : "Поражение...";
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

        public static void InitEntities()
        {
            AllTraders = Collections.Entities.AllTradersDefaultState.Values.ToList();
            AllFighters = Collections.Entities.AllFightersDefaultState.Values.ToList();
        }

        #endregion
    }
}
