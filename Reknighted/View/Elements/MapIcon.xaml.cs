using Reknighted.Controller;
using Reknighted.Controller.Collections;
using Reknighted.Model;
using Reknighted.Model.Entities;
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
using static System.Net.Mime.MediaTypeNames;

namespace Reknighted.View
{



    public partial class MapIcon : UserControl
    {
        private Point _position = new Point(0, 0);
        public Point Position { get { return _position; } set { _position = value; this.Margin = new Thickness(_position.X, _position.Y, 0, 0); } }

        private object _link;

        private string? _name;
        public string Name
        {
            get
            {   
                if (_name == null)
                {
                    if (Link is TraderModel)
                    {
                        return ((TraderModel)Link).Name;
                    }
                    else if (Link is Fighter)
                    {
                        return ((Fighter)Link).Name;
                    }
                    return "Undefined";
                }
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        public string Description
        {
            get
            {
                if (Name != string.Empty)
                    return Name;
                return "Undefined";
            }
        }

        public object Link
        {
            get => _link;
            set
            {
                _link = value;
            }
        }


        public MapIcon(IMappable link)
        {
            InitializeComponent();

            Position = link.Point;

            this.image.Source = new BitmapImage();

            string path = link.PathToIcon;

            if (link is Fighter)
            {
                if (((Fighter)link).IsDefeated)
                {
                    path = Items.PathTo("skull");
                }
            }

            LoadImage(path);
            Link = link;
        }


        public MapIcon(string name, Location link, string pathToIcon, Point position)
        {   
            InitializeComponent();
            Position = position;
            Name = name;

            this.image.Source = new BitmapImage();

            LoadImage(pathToIcon);
            Link = link;
        }

        private void LoadImage(string path_str)
        {
            var _image = new System.Windows.Controls.Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, path_str);
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            _image.Source = bitmap;

            this.image.Source = bitmap;
        }

        private void Action(object sender, MouseButtonEventArgs e)
        {   
            if (Link == null)
            {
                return;
            }
            if (Link.GetType() == typeof(TraderModel))
            {
                Game.CurrentTrader = (TraderModel)Link;

            }
            if (Link is Location)
            {   
                if ((Location)Link == Game.PlayerModel.Location)
                {
                    return;
                }

                var result = MessageBox.Show($"{Game.app.FindResource("msgTravelCost")} {Game.NextJorneyCosts[(Location)Link]} {Game.app.FindResource("valuta")}.\n\n{Game.app.FindResource("msgConfirmation")}", $"{Game.app.FindResource("travelHeader")}", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {   
                    if (Game.PlayerModel.Balance < Game.NextJorneyCosts[(Location)Link])
                    {
                        Game.Message((string)Game.app.FindResource("msgNoMoney"));
                        return;
                    }

                    Game.PlayerModel.Balance -= Game.NextJorneyCosts[(Location)Link];
                    Game.PlayerModel.Location = (Location)Link;

                    DialogLib.AwaitingMessage.ShowAwaitingMessage(Game.Window.grid, $"{Game.app.FindResource("msgRide")}");
                    Game.Window.location.groupBox.Header = Game.LocationString[(Location)Link];

                    Game.Window.location.Update();
                    Game.PlayerView.UpdateStats();
                    Game.GenerateJorneyCost();
                    
                    Game.Window.gameTabs.SelectedIndex = 0;
                    Game.Window.gameTabs.SelectedIndex = 1;
                }

            }
            if (Link.GetType() == typeof(Fighter))
            {
                Game.FightWith((Fighter)Link);
            }
        }

        private void image_MouseEnter(object sender, MouseEventArgs e)
        {   
            if (Game.Window != null)
            {
                Game.Window.LocationInfo = Description;
            }

        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Game.Window != null)
            {
                Game.Window.LocationInfo = string.Empty;
            }
        }
    }
}
