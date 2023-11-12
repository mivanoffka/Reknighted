using Reknighted.Images;
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

                if (value)
                {
                    gameTabs.SelectedIndex = 0;
                    knightTabButton.Header = "Торговля";
                }
                else
                {
                    knightTabButton.Header = "Рыцарь";
                    gameTabs.SelectedIndex = gameTabs.SelectedIndex;
                }

            }
        }


        public GameWindow()
        {
            InitializeComponent();
            DragAndDrop.InfoLabel = this.infoLabel;


            this.MouseMove += DragAndDrop.MouseMoveHandler;
            this.MouseUp += DragAndDrop.MouseUpHandler;

            defaultWindowHeight = this.Height;
            defaultGridHeight = this.grid.Height;
            defaultTabHeight = this.gameTabs.Height;
        }

        public void LoadPlayer()
        {
            this.playerView.PlayerModel = new PlayerModel();
            this.playerView.PlayerModel.Items[0] = new ItemModel(Collections.Items.Apple);
            this.playerView.PlayerModel.Items[1] = new ItemModel(Collections.Items.Sword);
            this.playerView.UpdateContent();
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

            }


            if (gameTabs.SelectedIndex == 0)
            {
                if (TradeMode)
                {
                    this.Height = defaultWindowHeight * 1.58;
                    this.grid.Height = defaultGridHeight * 1.65;
                    this.gameTabs.Height = defaultTabHeight * 1.65;
                }
                else
                {
                    this.Height = defaultWindowHeight;
                    this.grid.Height = defaultGridHeight;
                    this.gameTabs.Height = defaultTabHeight;
                }
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
            TradeMode = !TradeMode;
        }
    }
}
