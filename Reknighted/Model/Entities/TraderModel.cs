using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Reknighted.Controller;
using Reknighted.Model.Items;

namespace Reknighted.Model.Entities
{
    [JsonDerivedType(typeof(TraderModel))]
    public class TraderModel : ITradeable, IMappable
    {

        public Location City { get; set; }

        private TraderType type = TraderType.Universal;
        private string _name;

        public string Name
        {
            get => _name;
        }

        public TraderType Type
        {
            get => type;
        }

        public string PathToIcon { get; set; } = string.Empty;
        public System.Windows.Point Point { get; set; } = new System.Windows.Point(0, 0);

        private List<ItemModel?> _items = new List<ItemModel?>();
        private int _balance = 2000;

        [JsonConstructor]
        public TraderModel(Location city, string name, TraderType type, string pathToIcon, System.Windows.Point point, List<ItemModel> items, int balance)
        {
            City = city;
            _name = name;
            this.type = type;
            PathToIcon = pathToIcon;
            Point = point;
            _items = items;
            _balance = balance;
        }
        public TraderModel(TraderType type, string name, ItemModel?[] items, string pathToIcon, System.Windows.Point position, Location city)
        {
            City = city;
            ItemModel?[] copied = new ItemModel[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    copied[i] = items[i].Copy();
                }
                else
                {
                    copied[i] = null;
                }

            }

            this.type = type;
            _name = name;

            for (int i = 0; i < 27; i++)
            {
                Items.Add(null);
            }

            for (int i = 0; i < items.Length; i++)
            {
                copied[i].IsPossessed = false;
                AddItem(copied[i]);
            }

            Point = position;
            PathToIcon = pathToIcon;
        }

        public List<ItemModel?> Items
        {
            get
            {
                return _items;
            }
        }

        public int Balance
        {
            get
            {
                return _balance;
            }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _balance = value;
                Game.Update();
            }
        }

        public void AddItem(ItemModel? newItem)
        {
            int index = Items.IndexOf(null);
            Items[index] = newItem;
            Game.Update();
        }

        public void RemoveItem(ItemModel? item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == item)
                {
                    Items[i] = null;
                }
            }
            Game.Update();
        }

        public ItemModel? this[int index]
        {
            get
            {
                return ItemByIndex(index);
            }
            set
            {
                ItemModel? im = ItemByIndex(index);
                im = value;
            }
        }

        private ItemModel? ItemByIndex(int index)
        {
            if (index >= 0 && index < 27)
            {
                return Items[index];
            }
            else
            {
                throw new ArgumentException();
            }

        }
    }
}
