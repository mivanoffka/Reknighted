using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reknighted
{
    public partial class GameWindow : Window
    {
        double defaultWindowHeight = 0;
        double defaultGridHeight = 0;
        double defaultTabHeight = 0;

        bool _tradeMode = false;

        public bool TradeMode
        {
            get
            {
                return _tradeMode;
            }

            set
            {
                _tradeMode = value;
            }
        }


        public GameWindow()
        {
            InitializeComponent();
            Game.InfoLabel = this.balanceLabel;


            this.MouseMove += Game.MouseMoveHandler;
            this.MouseUp += Game.MouseUpHandler;

            defaultWindowHeight = this.Height;
            defaultGridHeight = this.grid.Height;
            defaultTabHeight = this.gameTabs.Height;
        }

        public void LoadPlayer()
        {
            var player = new PlayerModel();
            this.playerView.PlayerModel = player;
            Game.PlayerModel = player;

            this.playerView.PlayerModel.Items[0] = new FoodModel(Collections.Items.Apple);
            this.playerView.PlayerModel.Items[1] = new WeaponModel(Collections.Items.Sword);
            this.playerView.PlayerModel.Items[2] = new ArmorModel(Collections.Items.Helmet);
            //this.playerView.UpdateContent();
            Game.ResetAndUpdate();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<TabItem> list = new List<TabItem>();
            list.Add(mapTabButton);
            list.Add(locationTabButton);
            list.Add(knightTabButton);

            TabControl tabControl = (TabControl)sender;

            foreach (TabItem item in list)
            {

                if (item == tabControl.SelectedItem)
                {
                    item.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    item.Foreground = new SolidColorBrush(Colors.White);
                }

                Resize();
            }
        }

        public void Resize()
        {

            if (gameTabs.SelectedIndex == 0 && Game.CurrentTrader != null)
            {
                this.Height = defaultWindowHeight * 1.66;
                this.grid.Height = defaultGridHeight * 1.75;
                this.gameTabs.Height = defaultTabHeight * 1.75;

            }
            else
            {
                this.Height = defaultWindowHeight;
                this.grid.Height = defaultGridHeight;
                this.gameTabs.Height = defaultTabHeight;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            Game.CurrentTrader = Collections.Traders.Peter;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result_1 = MessageBox.Show("Вы уверены, что хотите вступить в бой? Это может стоить вам жизни. Ну или хотя бы 50 тугриков.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result_1 == MessageBoxResult.Yes)
            {
                if(playerView.PlayerModel.Balance < 50)
                {
                    MessageBox.Show("У вас слишком мало денег");
                    return;
                }
                Random random = new Random();
                int v = random.Next(0, 2);

                if (v == 0)
                {
                    MessageBox.Show("Вы трагически проиграли. Вам грозит позор до следующей битвы. Потом о вас все забудут.", "Поражение", MessageBoxButton.OK, MessageBoxImage.Hand);

                    gameTabs.SelectedIndex = 0;
                    playerView.PlayerModel.Balance -= 50;


                }

                else
                {
                    MessageBox.Show("Вы героически победили. Но не обольщайтесь, уже через час о вашем подвиге все забудут.", "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);

                    gameTabs.SelectedIndex = 0;

                    playerView.PlayerModel.AddItem(new ArmorModel(Collections.Items.HornsHelmet));
                    playerView.PlayerModel.Balance += 50;

                    //Game.ResetAndUpdate();
                    //playerView.UpdateContent();
                }

            }
        }
    }
}
