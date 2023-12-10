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

namespace Reknighted.Controller
{
    /// <summary>
    /// Логика взаимодействия для EnemyWindow.xaml
    /// </summary>
    public partial class EnemyWindow : Window
    {
        Fighter _enemy;
        public bool success = false;

        public int Bet { get => (int)betSlider.Value;  }
        public bool Success { get; set;  } = false;

        public EnemyWindow(Fighter enemy)
        {
            InitializeComponent();
            _enemy = enemy;
            enemyBox.Header = _enemy.Name;
            //errorLabel.Content = "";
            //MessageBox.Show("Шанс победы: " + ((int)(100 * Fighting.Fight(new double[] { Game.PlayerModel.Damage, Game.PlayerModel.Protection, Game.PlayerModel.HealthPercentage }, new double[] { enemy.Damage, enemy.Protection, enemy.HealthPercentage }))).ToString() + "%");
            //errorLabel.Content = "Шанс победы: " + ((int)(100 * Fighting.Fight(new double[] { Game.PlayerModel.Damage, Game.PlayerModel.Protection, Game.PlayerModel.HealthPercentage }, new double[] { enemy.Damage, enemy.Protection, enemy.HealthPercentage }))).ToString() + "%";
            betBox.Header = "Ставка    [ 50 ]";

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
                betBox.Header = "Ставка    [ " + Math.Round(betSlider.Value).ToString() + " ]";

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
                Game.Message("А денежек то не хватит...", MessageType.Error);
                Success = false;
                return;
            }


            if (player.HealthPercentage <= 0.2)
            {
                Game.Message("Нельзя сражаться в таком состоянии здоровья!", MessageType.Error);
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
            label.Text = "Идёт бой";
            Thread thread = new Thread(SleepThread);
            thread.Start();
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
