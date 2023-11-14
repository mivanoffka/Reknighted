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

namespace Reknighted.View
{
    public partial class TraderTemplate : UserControl
    {
        public TraderTemplate()
        {
            InitializeComponent();
            CreateAndPlace();
        }

        private void CreateAndPlace()
        {
            int startX = 15; int startY = 15;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cell cell = new Cell();
                    cell.Position = new Point(startX + j * cell.Width - 0.25 * cell.Width, startY + i * cell.Height - 0.25 * cell.Height);

                    inventoryGrid.Children.Add(cell);
                }

            }

        }
    }
}
