using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using System.Resources;
using System.Reflection;
using System.Text.Json;
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

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
            Dictionary<string, string> slotNames = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("lang\\names.txt"));
            foreach(ListBoxItem slot in saveSlots.Items)
            {
                slot.Content = slotNames[slot.Name];
            }
        }

        private void Slot_Changed(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены?", "", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.No)
            {
                return;
            }

            int i = saveSlots.Items.IndexOf(sender) + 1;
            if (isSaving)
            {
                FileManager.Save(i);
                MessageBox.Show($"Успешно сохранено в слот {i}");

                var jobj = JObject.Parse(File.ReadAllText("lang\\names.txt"));
                jobj.Remove(((ListBoxItem)sender).Name);
                jobj.Add(new JProperty(((ListBoxItem)sender).Name, $"{i} - {Game.PlayerModel.Location} - {DateTime.Now}"));
                File.WriteAllText("lang\\names.txt", jobj.ToString());
                ((ListBoxItem)sender).Content = $"{i} - {Game.PlayerModel.Location} - {DateTime.Now}";

            }
            else
            {
                ObjectsState? states = FileManager.LoadProgress(i);
                if(states == null)
                {
                    return;
                }

                GameWindow gameWindow = new GameWindow();
                Game.Window = gameWindow;
                gameWindow.Show();


                gameWindow.playerView.Model = states.Player;
                Game.PlayerModel = states.Player;
                Game.AllTraders = states.Traders;
                Game.AllFighters = states.Fighters;

                Game.Update();
                this.Close();
            }

        }
    }
}
