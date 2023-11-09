using Reknighted.Images;
using Reknighted.InfoTypes;
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

        public GameWindow()
        {
            InitializeComponent();
            DragAndDrop.InfoLabel = this.infoLabel;


            this.MouseMove += DragAndDrop.MouseMoveHandler;
            this.MouseUp += DragAndDrop.MouseUpHandler;

            int startX = 0; int startY = 60;

            //for (int i = 0; i < 4; i++)
            //{   
            //    for (int j = 0; j < 6; j++)
            //    {
            //        Cell cell = new Cell();
            //        cell.Position = new Point(startX + i * cell.Width, startY + j * cell.Height);
            //        knightGrid.Children.Add(cell);
            //    }
            //}

            startX = 0; startY = 0;


            for (int i = 0; i < 60; i++)
            {   
                Item item = new Item(ItemInfo.DefaultItem);
                item.Position = new Point(-20, -20);
                knightGrid.Children.Add(item);
            }

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<TabItem> list = new List<TabItem>();
            list.Add(mapTabButton);
            list.Add(locationTabButton);
            list.Add(knightTabButton);

            foreach (TabItem item in list)
            {
                TabControl tabControl = (TabControl)sender;

                if (item == tabControl.SelectedItem)
                {
                    item.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    item.Foreground = new SolidColorBrush(Colors.White);
                }

            }

            
        }
    }
}
