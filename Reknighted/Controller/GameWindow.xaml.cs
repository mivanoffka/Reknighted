using Reknighted.Controller;
using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using static Reknighted.View.MapIcon;

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


        public string LocationInfo { get => locationInfoLabel.Content.ToString(); set => locationInfoLabel.Content = value; }

        public GameWindow()
        {
            InitializeComponent();

            this.MouseMove += Game.MouseMoveHandler;
            this.MouseUp += Game.MouseUpHandler;

            defaultWindowHeight = this.Height;
            defaultGridHeight = this.grid.Height;
            defaultTabHeight = this.gameTabs.Height;

            globalMap.MapIcons = Collections.Entities.GlobalMap.Values.ToList();
        }

        public void LoadPlayer(Faction faction)
        {
            var player = new PlayerModel(faction);
            this.playerView.Model = player;
            Game.PlayerModel = player;

            this.playerView.Model.Items[0] = Collections.Items.Food["apple_green"].Copy();
            this.playerView.Model.Items[1] = Collections.Items.Weapons["veteran_sword"].Copy();
            this.playerView.Model.Items[2] = Collections.Items.Armors["leather_hat"].Copy();
            //this.playerView.UpdateContent();
            Game.Update();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gameTabs.SelectedIndex != 0)
            {
                Game.CurrentTrader = null;
            }


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

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                SaveWindow saveWindow = new SaveWindow();
                saveWindow.isSaving = true;
                saveWindow.ShowDialog();
                e.Handled = true;
            }
        }
    }
}
