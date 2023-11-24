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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reknighted.View
{
    public partial class TraderTemplate : UserControl
    {
        private TraderModel? _model = null;

        public TraderModel? Model
        {
            get => _model;
            set
            {
                _model = value;

                if (value != null)
                {
                    groupBox.Header = _model.Name;
                    UpdateContent();
                }

            }
        }

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
                    cell.IsPossessed = false;
                    cell.Position = new Point(startX + j * cell.Width - 0.25 * cell.Width, startY + i * cell.Height - 0.25 * cell.Height);

                    Game.AllCells.Add(cell);
                    Game.TraderCells.Add(cell);
                    inventoryGrid.Children.Add(cell);
                }
            }

        }

        public void UpdateContent()
        {
            if (_model != null)
            {
                for (int i = 0; i < _model.Items.Count; i++)
                {
                    ItemModel? itemModel;
                    itemModel = _model.Items[i];

                    if (itemModel != null)
                    {
                        ItemView itemView = new ItemView(itemModel);
                        if (this.Parent != null)
                        {
                            try
                            {
                                Grid grid = (Grid)this.Parent;
                                grid.Children.Add(itemView);

                                itemView.PlaceToCell(Game.TraderCells[i]);

                            }
                            catch (Exception exception)
                            {

                                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                throw;
                            }
                        }
                    }
                }
            }
        }
    }
}
