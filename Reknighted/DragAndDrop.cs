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

namespace Reknighted
{

    public class DragAndDrop
    {
        public static Point? MousePreviousPosition = null;
        public static Point? MouseCurrentPosition = null;

        private static List<ItemView>  _items = new List<ItemView>();
        private static List<Cell> _cells = new List<Cell>();
        private static Cell? _selectedCell = null;
        private static Window? _window = null;
        private static ItemView? _item = null;
        private static bool _isDragging = false;
        private static System.Windows.Controls.Label? _infoLabel = null;

        public static System.Windows.Controls.Label? InfoLabel { get { return _infoLabel; } set { _infoLabel = value; } }

        public static Window Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static ItemView? Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public static List<Cell> Cells
        {
            get { return _cells; }
        }

        public static Point? delta = null;

        public static List<ItemView> Items { get { return _items; } }
        public static void MouseMoveHandler(object sender, MouseEventArgs e)
        {   
            if (delta == null)
            {
                if (_cells[0] != null && _cells[1] != null)
                {
                    Point point_0 = _cells[0].PointToScreen(new Point(0, 0));
                    Point point_1 = _cells[1].PointToScreen(new Point(0, 0));
                    delta = new Point(Math.Abs(point_0.X - point_1.X), 0);

                    MessageBox.Show(delta.Value.X.ToString());
                }
            }

            MousePreviousPosition = MouseCurrentPosition;
            MouseCurrentPosition = e.GetPosition(_window);

            GameWindow? gameWindow = (GameWindow?)_window;
            if (gameWindow != null)
            {
                if (gameWindow.gameTabs.SelectedIndex == 0)
                {
                    List<Cell> cells = new List<Cell>();

                    foreach (Cell cell in Cells)
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

                    if (Item != null)
                    {
                        Item.Position = (Point)MouseCurrentPosition;
                        Item.Position = new Point(Item.Position.X - Item.Width, Item.Position.Y - 50);
                    }

                }
            }



        }

        public static void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            GameWindow? gameWindow = (GameWindow?)_window;
            if (gameWindow != null)
            {
                if (gameWindow.gameTabs.SelectedIndex == 0)
                {
                    if (Item != null)
                    {
                        if (_selectedCell != null)
                        {

                            int prev_index = -1;
                            if (Item.Cell != null)
                                prev_index = Cells.IndexOf(Item.Cell);

                            Item.PlaceToCell(_selectedCell);

                            int cur_index = -1;
                            if (Item.Cell != null)
                                cur_index = Cells.IndexOf(Item.Cell);

                            //MessageBox.Show("Prev: " + prev_index + "; Cur: " + cur_index);

                            if (prev_index >= 27 )
                            {
                                prev_index -= 27;
                                gameWindow.playerView.PlayerModel.EquippedItems[prev_index] = null;
                            }
                            else
                            {
                                gameWindow.playerView.PlayerModel.Items[prev_index] = null;
                            }

                            if (cur_index >= 27)
                            {
                                cur_index -= 27;
                                gameWindow.playerView.PlayerModel.EquippedItems[cur_index] = Item.ItemModel;
                            }
                            else
                            {
                                gameWindow.playerView.PlayerModel.Items[cur_index] = Item.ItemModel;
                            }

                            foreach (var item in Items)
                            {
                                Grid grid = (Grid)item.Parent;
                                grid.Children.Remove(item);
                            }

                            foreach (var cell in Cells)
                            {
                                cell.ContentItem = null;
                            }

                            Items.Clear();
                            gameWindow.playerView.UpdateContent();

                        }

                        Item.Opacity = 1;
                        Item = null;
                    }
                }
            }

        }   

        
    }   

}
