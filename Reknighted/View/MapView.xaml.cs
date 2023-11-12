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

            masqueradeButton.ToolTip = "Столица Карточного Королевства\nи резиденция великой династии\nАрлекинов. Вернее, того, что от\nнеё осталось...";
        }

        public void Travel()
        {
            var result = MessageBox.Show("На дорогу вам придётся потратить 100 тугриков. Вы готовы?", "Путешествие", MessageBoxButton.YesNo, MessageBoxImage.Question);


        }
    }
}
