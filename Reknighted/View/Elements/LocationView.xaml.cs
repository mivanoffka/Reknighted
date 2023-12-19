using Newtonsoft.Json.Linq;
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
using static Reknighted.Controller.Game;

namespace Reknighted.View
{
    /// <summary>
    /// Логика взаимодействия для LocationView.xaml
    /// </summary>
    public partial class LocationView : UserControl
    {   
        private List<MapIcon?> mapIcons;

        public void Update()
        {
            List<MapIcon> lst = new();
            foreach (var item in Controller.Game.AllTraders)
            {
                if (item.City == Controller.Game.PlayerModel.Location)
                {
                    lst.Add(new MapIcon((IMappable)item));
                }
            }
            foreach (var item in Controller.Game.AllFighters)
            {
                if (item.City == Controller.Game.PlayerModel.Location)
                {
                    lst.Add(new MapIcon((IMappable)item));
                }
            }

            MapIcons = lst;
        }

        public void RemoveEntity(IMappable entity)
        {
            foreach (var item in mapIcons)
            {
                if (item.Link == entity)
                {
                    grid.Children.Remove(item);
                }
            }
        }


        public List<MapIcon?> MapIcons { get { return mapIcons; } 
            set
            {   
                if (mapIcons is not null)
                {
                    foreach (var icon in mapIcons)
                    {
                        grid.Children.Remove(icon);
                    }
                }

                mapIcons = value;

                if (mapIcons is not null)
                {
                    foreach (var icon in mapIcons)
                    {
                        Grid parent = (Grid)icon.Parent;
                        if (parent != null)
                        {
                            if (parent.Children.Contains(icon))
                            {
                                parent.Children.Remove(icon);
                            }
                        }


                        if (!grid.Children.Contains(icon)) 
                           grid.Children.Add(icon);
                    }

                }
            } 
        }

        public string Info
        {
            set
            {
                
            }
        }


        public LocationView()
        {
            InitializeComponent();
        }
    }
}
