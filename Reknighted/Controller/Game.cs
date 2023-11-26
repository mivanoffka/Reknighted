using Reknighted.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Reknighted.Model;
using Reknighted.Collections;

namespace Reknighted.Controller
{

    public class Game
    {
        public static Point? MousePreviousPosition = null;
        public static Point? MouseCurrentPosition = null;

        private static List<ItemView> _items = new List<ItemView>();

        public static PlayerView? PlayerView = null;
        public static PlayerModel? PlayerModel = null;
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

                        gw.Resize();
                    }



                }

            }
        }

        private static List<Cell> _allCells = new List<Cell>();
        private static Cell? _selectedCell = null;
        private static List<Cell> _inventoryCells = new List<Cell>();
        private static List<Cell> _equipmentCells = new List<Cell>();
        private static List<Cell> _traderCells = new List<Cell>();

        public static List<Cell> InventoryCells { get { return _inventoryCells; } }
        public static List<Cell> EquipmentCells { get { return _equipmentCells; } }
        public static List<Cell> TraderCells { get { return _traderCells; } }


        private static GameWindow? _window = null;
        private static ItemView? _item = null;
        private static bool _isDragging = false;
        private static System.Windows.Controls.Label? _infoLabel = null;


        public static System.Windows.Controls.Label? damageLabel = null;
        public static System.Windows.Controls.Label? protectionLabel = null;
        public static System.Windows.Controls.Label? healthLabel = null;
        public static System.Windows.Controls.Label? fortuneLabel = null;

        public static double Scale = -1;

        public static System.Windows.Controls.Label? InfoLabel { get { return _infoLabel; } set { _infoLabel = value; } }

        public static GameWindow Window
        {
            get { return _window; }
            set
            {
                _window = value;
                var gw = _window as GameWindow;
                PlayerView = gw!.playerView;
            }
        }

        public static ItemView? Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public static List<Cell> AllCells
        {
            get { return _allCells; }
        }

        public static Point? delta = null;

        public static List<ItemView> Items { get { return _items; } }

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

                        if (InfoLabel != null)
                        {
                            Point cellPos = _selectedCell.PointToScreen(new Point(0, 0));
                            //InfoLabel.Content += (cellPos.X).ToString() + ", " + (cellPos.Y).ToString();
                        }
                    }

                    // Центрирование предмета при перетаскивании
                    if (Item != null && Item.Model.IsPossessed)
                    {
                        // Да это костыль но по другому я не знаю как
                        Item.Position = new Point(
                            MouseCurrentPosition.Value.X - Item.ActualWidth / 1.7,
                            MouseCurrentPosition.Value.Y - Item.ActualHeight * 1.15
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
                    if (Item != null)
                    {
                        if (_selectedCell != null)
                        {
                            Item.Model.MoveToCell(_selectedCell);
                        }

                        ResetAndUpdate();

                        Item.Opacity = 1;
                        Item = null;
                    }
                }
            }

        }

        public static void ResetAndUpdate()
        {
            if (PlayerModel != null)
            {
                PlayerModel.UpdateStats();
            }

            if (_window == null)
            {
                return;
            }

            try
            {
                foreach (var item in Items)
                {
                    Grid grid = (Grid)item.Parent;
                    grid.Children.Remove(item);
                }

                foreach (var cell in AllCells)
                {
                    cell.ContentItem = null;
                }

                Items.Clear();

                GameWindow gameWindow = _window;
                gameWindow.playerView.UpdateContent();
                if (CurrentTrader != null)
                {
                    gameWindow.traderView.UpdateContent();
                }


                InfoLabel.Content = "Деньги: " + gameWindow.playerView.PlayerModel.Balance;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

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

                    delta = new Point(0, 0);
                }
            }


        }

        public static void Error(string message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
