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
using Reknighted.Controller;
using Reknighted.Model;

namespace Reknighted
{
    /// <summary>
    /// Логика взаимодействия для SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        public SelectionWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickPlaceHolder(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            Game.Window = gameWindow;
            gameWindow.Show();
            
            Dictionary<Button, Faction> factions = new() { {heartsButton, Faction.Hearts }, {spadesButton, Faction.Spades }, {clubsButton, Faction.Clubs}, {diamondsButton, Faction.Diamonds} };
            Faction faction = factions[sender as Button];
            gameWindow.LoadPlayer(faction);

            this.Close();
        }
    }
}
