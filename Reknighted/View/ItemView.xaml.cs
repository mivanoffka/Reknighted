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
            Game.AllItemsViews.Add(this);


            ToolTipService.SetInitialShowDelay(this, 250);
            ToolTipService.SetBetweenShowDelay(this, 250);


            this._model = itemInfo;
            this.image.Source = itemInfo.Image.Source;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Game.PlayerView.HideInfo();

            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (Game.CurrentTrader != null && Game.PlayerView?.Model != null)
                {   
                    if (Game._productItem == this)
                    {   
                        ITradeable? customer = Model.IsPossessed ? Game.CurrentTrader : Game.PlayerModel;
                        if (customer != null)
                        {
                            this.Model.SellTo(customer);
                        }
                    }
                    else
                    {
                        Game._productItem = this;
                        Game.Message($"{Game.app.FindResource("msgConfirmAction")}");
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
                    Game.SelectedItem = this;
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

        public void Place(Window? window = null)
        {   
            if (window == null)
            {
                window = Game.Window;
            }

            if (this.Model.Cell != null)
            {
                var winPos = window.PointToScreen(new Point(0, 0));
                var cellPos = this.Model.Cell.PointToScreen(new Point(0, 0));

                // -6, -28
                this.Position = new Point((cellPos.X - winPos.X) / Game.Scale - 6, (cellPos.Y - winPos.Y) / Game.Scale - 28);
            }


        }

        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            string message = string.Empty;
            if (Game.CurrentTrader != null)
            {
                string word = Model.IsPossessed ? $"{Game.app.FindResource("txtSell")}" : $"{Game.app.FindResource("txtBuy")}";
                message = Model.Price + $" {Game.app.FindResource("valuta")}";
            }
            else
            {
                message = Model.Help();
            }

            if (Game.PlayerView != null && Model != null)
                if (e.LeftButton != MouseButtonState.Pressed)
                    Game.PlayerView.ShowInfo(Model);

            Game.Message(message);

        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            Game.Message(string.Empty);

            if (Game.PlayerView != null && Model != null)
                Game.PlayerView.HideInfo();
        }
    }
}
