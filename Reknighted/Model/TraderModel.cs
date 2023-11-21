using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class TraderModel
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
        private int _balance = 100;

        public TraderModel(TraderType type, string name = "Торговец")
        {
            this.type = type;
            _name = name;
        }

        public TraderModel(TraderType type, List<ItemModel?> items, int balance)
        {
            this.type = type;
            _items = items;
            Balance = balance;
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
    }
}
