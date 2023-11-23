using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class TraderModel : ITradeable
    {
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

        private List<ItemModel?> _items = new List<ItemModel?>();
        private int _balance = 1000;

        public TraderModel(TraderType type, string name = "Торговец")
        {
            this.type = type;
            _name = name;

            for (int i = 0; i < 27; i++)
            {
                this.Items.Add(null);
            }
        }

        public TraderModel(TraderType type, List<ItemModel?> items, int balance)
        {
            this.type = type;
            _items = items;
            Balance = balance;

            for (int i = 0; i < 27; i++)
            {
                this.Items.Add(null);

            }
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
                Game.ResetAndUpdate();
            }
        }

        public void AddItem(ItemModel? newItem)
        {
            int index = Items.IndexOf(null);
            Items[index] = newItem;
            Game.ResetAndUpdate();
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
            Game.ResetAndUpdate();
        }
    }
}
