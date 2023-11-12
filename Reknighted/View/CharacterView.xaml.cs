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

namespace Reknighted
{
    /// <summary>
    /// Логика взаимодействия для Character.xaml
    /// </summary>
    public partial class Character : UserControl
    {
        public Character()
        {
            InitializeComponent();

            int startX = 15; int startY = 0;

            for (int i = 0; i < 3; i++)
            {
                Cell cell = new Cell();
                cell.Position = new Point(startX + i * cell.Width - 0.25 * cell.Width, startY);

                equipmentGrid.Children.Add(cell);
            }


            startX = 15; startY = 15;

            for (int i = 0; i < 9; i++)
            {   
                for (int j = 0; j < 3; j++)
                {
                    Cell cell = new Cell();
                    cell.Position = new Point(startX + i * cell.Width - 0.25 * cell.Width, startY + j * cell.Height - 0.25 * cell.Height);

                    inventoryGrid.Children.Add(cell);
                }

            }
        }
    }
}
