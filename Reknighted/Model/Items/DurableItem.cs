using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Reknighted.Controller.Game;

namespace Reknighted.Model.Items
{
    public abstract class DurableItem : ItemModel
    {
        protected int _maxDurability;
        protected int _currentDurability;

        public int MaxDurability
        {
            get
            {
                return _maxDurability;
            }
            set
            {
                _maxDurability = value;
            }
        }

        public int CurrentDurability
        {
            get
            {
                return _currentDurability;
            }

            set
            {
                if (value < MaxDurability)
                {
                    if (value > 0)
                    {
                        _currentDurability = value;
                    }
                    else
                    {
                        _currentDurability = 0;
                        Controller.Game.PlayerModel.RemoveItem(this);
                        Update();
                    }
                }
                else
                {
                    _currentDurability = MaxDurability;
                }
            }
        }

        [JsonIgnore]
        public double DurabilityPercentage
        {
            get
            {
                return _currentDurability / (double)_maxDurability;
            }

            set
            {
                _currentDurability = (int)(value * _maxDurability);
            }

        }

        public DurableItem() : base() { }

        public DurableItem(string name, string description, int basePrice, int maxDurability, string imageSource = "") : base(name, description, basePrice, imageSource)
        {
            _maxDurability = maxDurability;
            _currentDurability = _maxDurability;
        }

        [JsonConstructor]
        public DurableItem(int maxDurability, int currentDurability,
            string pathToImage, string name, string description, int price, bool isPossesed, Cell cell)
        {
            _maxDurability = maxDurability;
            _currentDurability = currentDurability;
            PathToImage = pathToImage;
            _name = name;
            _description = description;
            _price = price;
            _isPossessed = isPossesed;
            Cell = cell;
        }

    }
}
