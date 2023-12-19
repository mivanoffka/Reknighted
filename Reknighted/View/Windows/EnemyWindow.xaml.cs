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
using System.Threading;
using Reknighted.Model.Entities;

namespace Reknighted.Controller
{
    /// <summary>
    /// Логика взаимодействия для EnemyWindow.xaml
    /// </summary>
    public partial class EnemyWindow : Window
    {
        private readonly Color blue = Color.FromRgb(181, 198, 202);
        private readonly Color yellow = Color.FromRgb(202, 200, 181);
        private readonly Color red = Color.FromRgb(199, 158, 158);

        Fighter _enemy;
        public bool success = false;
        public int Bet { get => (int)betSlider.Value; }
        public bool Success { get; set;  } = false;

        public EnemyWindow(Fighter enemy)
        {
            InitializeComponent();
            _enemy = enemy;
            enemyBox.Header = _enemy.Name;
            //errorLabel.Content = "";
            label2.Visibility = Visibility.Collapsed;

            if (enemy.MoneyReward > -1)
            {
                enemyGrid.Children.Remove(betBox);
                label2.Visibility = Visibility.Visible;
                betSlider.Value = 0;
            }
            
            if (enemy.MoneyReward > 700)
            {
                label2.Text = "Страшно, вырубай...";
            }

            //int chance = ((int)(100 * Fighting.Fight(new double[] { Game.PlayerModel.Damage, Game.PlayerModel.Protection, Game.PlayerModel.HealthPercentage }, new double[] { enemy.Damage, enemy.Protection, enemy.HealthPercentage })));
           //MessageBox.Show("Шанс победы: " + ((int)(100 * Game.CallFightFromLib(new double[] { Game.PlayerModel.Damage, Game.PlayerModel.Protection, Game.PlayerModel.HealthPercentage }, new double[] { enemy.Damage, enemy.Protection, enemy.HealthPercentage }))).ToString() + "%");
                //errorLabel.Content = "Шанс победы: " + ((int)(100 * Fighting.Fight(new double[] { Game.PlayerModel.Damage, Game.PlayerModel.Protection, Game.PlayerModel.HealthPercentage }, new double[] { enemy.Damage, enemy.Protection, enemy.HealthPercentage }))).ToString() + "%";

            CreateAndPlace();
        }

        public void CreateAndPlace()
        {
            int delta_x = 12;
            int delta_y = 16;

            for (int i = 0; i < 3; i++)
            {
                var item = _enemy.EquippedItems[i];
                Cell cell = new Cell();
                cell.Position = new Point(delta_x + i * 45, delta_y);
                enemyGrid.Children.Add(cell);
                Game.AllCells.Remove(cell);

                if (item != null)
                {

                    ItemView itemView = new ItemView(item.Copy());
                    itemView.Position = new Point((int)(delta_x * 1.6) + i * 45 + 1, (int)(delta_y * 1.4) + 1); 
                    enemyGrid.Children.Add(itemView);
                    
                }
            }

        }

        private void betSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (betBox != null && betSlider != null)
            {
                betBox.Header = $"{Game.app.FindResource("betBox")}  {Math.Round(betSlider.Value).ToString()} ]";

            }
        }

        private void fightButton_Click(object sender, RoutedEventArgs e)
        {   
            PlayerModel? player = Game.PlayerModel;
            if (player == null)
                return;

            int bet = (int)betSlider.Value;
            
            if (player.Balance < bet)
            {
                Game.Message($"{Game.app.FindResource("msgNoMoney")}", MessageType.Error);
                Success = false;
                return;
            }


            if (player.HealthPercentage <= 0.2)
            {
                Game.Message($"{Game.app.FindResource("lowHealthMessage")}", MessageType.Error);
                Success = false;
                return;
            }


            List<System.Windows.UIElement> toRemove = new List<System.Windows.UIElement>();

            foreach (var item in grid.Children)
            {
                if (item != rect && item != label)
                {
                    toRemove.Add((System.Windows.UIElement)item);
                }
            }

            foreach (var item in toRemove)
            {
                grid.Children.Remove(item);
            }

            this.Height = 100;
            this.rect.Height = 40;

            Success = true;
            Close();
            //label.Text = "Идёт бой";
            //Thread thread = new Thread(SleepThread);
            //thread.Start();
        }


        private void SleepThread()
        {
            Thread.Sleep(250);
            Dispatcher.Invoke(IterFighting);
            Thread.Sleep(250);
            Dispatcher.Invoke(IterFighting);
            Thread.Sleep(250);
            Dispatcher.Invoke(IterFighting);
            Thread.Sleep(250);
            Dispatcher.Invoke(Close);
        }

        public void IterFighting()
        {
            this.label.Text += ".";
        }

    }
}
