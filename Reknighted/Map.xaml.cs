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
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {   
        public string CurrentTown
        {
            get
            {
                string txt = (string)checkedOne.Content;
                txt = txt.Split(' ')[0];
                return txt;
            }
        }

        List<RadioButton> radioButtons = new List<RadioButton>();
        RadioButton checkedOne = new RadioButton();

        public Map()
        {
            InitializeComponent();
            masqueradeButton.ToolTip = "Столица Карточного Королевства\nи резиденция великой династии\nАрлекинов. Вернее, того, что от\nнеё осталось...";

            this.radioButtons.Add(masqueradeButton);
            this.radioButtons.Add(heartsButton);
            this.radioButtons.Add(clubsButton);
            this.radioButtons.Add(spadesButton);
            this.radioButtons.Add(diamondsButton);
        }

        private void Travel(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton radio in radioButtons)
            {   
                radio.IsChecked = false;
            }

            var result = MessageBox.Show("На дорогу вам придётся потратить 100 тугриков. Вы готовы?", "Путешествие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                checkedOne = (RadioButton)sender;
            }
            
            checkedOne.IsChecked = true;
        }
    }
}
