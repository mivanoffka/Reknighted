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

namespace Reknighted.View
{
    /// <summary>
    /// Логика взаимодействия для LocationView.xaml
    /// </summary>
    public partial class LocationView : UserControl
    {   
        private List<MapIcon?> mapIcons;

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
                    foreach (var i in Collections.Locations.CityMaps)
                    {
                        if (i.Value == mapIcons)
                        {
                            this.groupBox.Header = i.Key;
                            break;
                        }
                        else
                        {
                            this.groupBox.Header = "Карточное Королевство";
                        }
                    }

                    foreach (var icon in mapIcons)
                    {   
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
