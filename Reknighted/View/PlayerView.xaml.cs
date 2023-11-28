using Reknighted.Controller;
using Reknighted.Model;
using Reknighted.View;
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
using static System.Net.Mime.MediaTypeNames;

namespace Reknighted
{
    public partial class PlayerView : UserControl
    {
        PlayerModel? _playerModel;

        public PlayerModel? Model
        {
            set
            {
                _playerModel = value;
            }
            get
            {
                return _playerModel;
            }
        }

        //List<Cell> _inventoryCells = new List<Cell>();
        List<Cell> _equipmentCells = new List<Cell>();

        public void UpdateStats()
        {
            try
            {
                damageLabel.Content = Model.Damage.ToString();
                armorLabel.Content = Model.Protection.ToString();
                healthLabel.Content = Model.CurrentHealth.ToString() + " (" + Math.Round(Model.HealthPercentage * 100) + "%)";
                balanceLabel.Content = Model.Balance.ToString();
            }
            catch
            {

            }
        }

        public PlayerView()
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

                    Game.InventoryCells.Add(cell);
                    inventoryGrid.Children.Add(cell);
                }

            }

            startX = 15; startY = 0;


            Type[] types = new Type[3];
            types[0] = typeof(WeaponModel);
            types[1] = typeof(ArmorModel);
            types[2] = typeof(ArtefactModel);   

            for (int i = 0; i < 3; i++)
            {
                Cell cell = new Cell();
                cell.Filter = types[i];
                cell.Position = new Point(startX + i * cell.Width - 0.25 * cell.Width, startY);
                
                Game.InventoryCells.Add(cell);
                Game.EquipmentCells.Add(cell);
                equipmentGrid.Children.Add(cell);
            }
        }

        public void UpdateContent()
        {
            Game.CalculateCoeff();

            if (_playerModel != null)
            {
                for (int i = 0; i < _playerModel.Items.Count + _playerModel.EquippedItems.Count(); i++)
                {
                    ItemModel? itemModel;
                    if (i < 27)
                    {
                        itemModel = _playerModel.Items[i];
                    }
                    else
                    {
                        itemModel = _playerModel.EquippedItems[i-27];
                    }

                    if (itemModel != null)
                    {
                        ItemView itemView = new ItemView(itemModel);
                        if (this.Parent != null)
                        {   
                            try
                            {
                                Grid grid = (Grid)this.Parent;
                                grid.Children.Add(itemView);

                                if (i < 27)
                                {
                                    itemView.PlaceToCell(Game.InventoryCells[i]);
                                }

                                else
                                {
                                    itemView.PlaceToCell(Game.EquipmentCells[i-27]);
                                }

                            }
                            catch (Exception exception) 
                            {
                                
                                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            } 
        }
    }
}
