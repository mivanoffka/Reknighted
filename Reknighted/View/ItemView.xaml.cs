using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reknighted
{

    public partial class ItemView : UserControl
    {   
        private bool _isClicked = false;
        private Point _position = new Point(0, 0);
        private ItemModel _itemInfo;

        public ItemModel ItemModel
        {
            get { return _itemInfo; }
        }

        private Cell? _cell = null;

        public Cell? Cell
        {
            get
            {
                return _cell;
            }
        }


        public Point Position { get { return _position; } set { _position = value; this.Margin = new Thickness(_position.X, _position.Y, 0, 0); } }

        public ItemView(ItemModel itemInfo)
        {
            InitializeComponent();
            Game.Items.Add(this);


            this._itemInfo = itemInfo;
            this.ToolTip = this._itemInfo.Information();
            this.image.Source = itemInfo.Image.Source;
        }

        ~ItemView()
        {
            Game.Items.Remove(this);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Game.Item = this;
            this.Opacity = 0.5;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {   

        }

        public void PlaceToCell(Cell cell)
        {
            if (cell.ContentItem == null)
            {   
                if (_cell != null)
                {
                    _cell.ContentItem = null;
                }
                _cell = cell;
                cell.ContentItem = this;
            }

            Place();

        }

        private void Place()
        {
            if (this._cell != null)
            {
                var winPos = Game.Window.PointToScreen(new Point(0, 0));
                var cellPos = _cell.PointToScreen(new Point(0, 0));

                // -6, -28
                this.Position = new Point((cellPos.X - winPos.X) / Game.Scale - 6, (cellPos.Y - winPos.Y) / Game.Scale - 28);
            }


        }
    }
}
