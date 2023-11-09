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

    public partial class Cell : UserControl
    {
        private Point _position = new Point(0, 0);
        private bool _isPointed = false;

        public Point Position
        {
            get 
            {
                return _position;
            } 
            set
            { 
                _position = value;
                this.Margin = new Thickness(_position.X, _position.Y, 0, 0);
            } 
        }

        public bool IsPointed
        {
            get
            {
                return _isPointed;
            }
            set
            {
                _isPointed = value;

                if (value == true)
                {

                    this.Opacity = 1d;
                    this.border.BorderThickness = new Thickness(1.5);
                }
                else
                {
                    this.Opacity = 0.75d;
                    this.border.BorderThickness = new Thickness(1);
                }
            }
        }

        public Cell() : this(new Point(0, 0))
        {

        }

        public Cell(Point position)
        {
            DragAndDrop.Cells.Add(this);
            Position = position;
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
