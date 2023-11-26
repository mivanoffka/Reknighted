using Reknighted.Controller;
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
        private ItemModel _model;

        public ItemModel Model
        {
            get { return _model; }
        }

        public Point Position { get { return _position; } set { _position = value; this.Margin = new Thickness(_position.X, _position.Y, 0, 0); } }

        public ItemView(ItemModel itemInfo)
        {
            InitializeComponent();
            Game.Items.Add(this);


            this._model = itemInfo;
            this.ToolTip = this._model.Information();
            this.image.Source = itemInfo.Image.Source;
        }


        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {   
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (Game.CurrentTrader != null && Game.PlayerView?.PlayerModel != null)
                {
                    ITradeable? customer = Model.IsPossessed ? Game.CurrentTrader : Game.PlayerModel;
                    if (customer != null)
                    {
                        this.Model.SellTo(customer);
                    }

                }
                else
                {
                    this.Model.Use();
                }

            }
            else
            {
                if (this.Model.IsPossessed)
                {
                    Game.Item = this;
                }
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {   

        }

        

        public void PlaceToCell(Cell newCell)
        {   
            if (this.Model.IsPossessed == newCell.IsPossessed)
            {
                if (newCell.ContentItem == null)
                {
                    if (Model.Cell != null)
                    {
                        Model.Cell.ContentItem = null;
                    }
                    Model.Cell = newCell;
                    newCell.ContentItem = this.Model;
                }
            }

            Place();
        }

        private void Place()
        {
            if (this.Model.Cell != null)
            {
                var winPos = Game.Window.PointToScreen(new Point(0, 0));
                var cellPos = this.Model.Cell.PointToScreen(new Point(0, 0));

                // -6, -28
                this.Position = new Point((cellPos.X - winPos.X) / Game.Scale - 6, (cellPos.Y - winPos.Y) / Game.Scale - 28);
            }


        }
    }
}
