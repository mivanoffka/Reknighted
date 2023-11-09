using Reknighted.InfoTypes;
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

    public partial class Item : UserControl
    {   
        private bool _isClicked = false;
        private Point _position = new Point(0, 0);
        private ItemInfo _itemInfo;

        public Point Position { get { return _position; } set { _position = value; this.Margin = new Thickness(_position.X, _position.Y, 0, 0); } }

        public Item(ItemInfo itemInfo)
        {
            InitializeComponent();
            DragAndDrop.Items.Add(this);


            this._itemInfo = itemInfo;
            this.ToolTip = this._itemInfo.Information();
            this.image.Source = itemInfo.Image.Source;
        }

        ~Item()
        {
            DragAndDrop.Items.Remove(this);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragAndDrop.Item = this;
            this.Opacity = 0.5;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {   

        }
    }
}
