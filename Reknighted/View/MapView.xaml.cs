using Reknighted.Controller;
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
    public partial class WorldMap : UserControl
    {   


        public WorldMap()
        {
            InitializeComponent();

            masqueradeButton.ToolTip = Game.app.FindResource("masqueradeTip");
        }

        public void Travel()
        {
            var result = MessageBox.Show($"{Game.app.FindResource("travelPromt")}", $"{Game.app.FindResource("travelHeader")}", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
