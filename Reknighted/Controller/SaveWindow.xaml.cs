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
using System.Windows.Shapes;

namespace Reknighted.Controller
{
    /// <summary>
    /// Логика взаимодействия для SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        public bool isSaving;
        public SaveWindow()
        {
            InitializeComponent();
        }

        private void Slot_Changed(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены?", "", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.No)
            {
                return;
            }

            int i = saveSlots.Items.IndexOf(sender);
            if (isSaving)
            {
                FileManager.Save(i);
                MessageBox.Show($"Успешно сохранено в слот {i}");
                //((ListBoxItem)sender).Content = $"{i} - {Game.PlayerModel.Location} - {DateTime.Now}";
            }
            else
            {
                PlayerModel player = FileManager.LoadProgress(i);
                if(player == null)
                {
                    return;
                }

                GameWindow gameWindow = new GameWindow();
                Game.Window = gameWindow;
                gameWindow.Show();

                gameWindow.LoadPlayer(player);
                this.Close();
            }

        }
    }
}
