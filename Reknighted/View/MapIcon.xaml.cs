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
using static System.Net.Mime.MediaTypeNames;

namespace Reknighted.View
{



    public partial class MapIcon : UserControl
    {
        private Point _position = new Point(0, 0);
        public Point Position { get { return _position; } set { _position = value; this.Margin = new Thickness(_position.X, _position.Y, 0, 0); } }

        private object _link;

        public string Name { get; set; } = String.Empty;
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

            LoadImage(link.PathToIcon);
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
                List<MapIcon> newList = new List<MapIcon>();

                foreach (var item in Game.AllTraders)
                {
                    if (item.City == (Location)Link)
                    {
                        newList.Add(new MapIcon(item));
                    }
                }

                foreach (var item in Game.AllFighters)
                {
                    if (item.City == (Location)Link)
                    {
                        newList.Add(new MapIcon(item));
                    }
                }

                Game.LocationView.MapIcons = newList;
                Game.Window.gameTabs.SelectedIndex = 1;
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
