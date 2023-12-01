using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reknighted.Controller.Game;

namespace Reknighted.Model
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
                        Reknighted.Controller.Game.PlayerModel.RemoveItem(this);
                        Reknighted.Controller.Game.Update();
                    }
                }
                else
                {
                    _currentDurability = MaxDurability;
                }
            }
        }

        public double DurabilityPercentage
        {
            get
            {
                return (double)_currentDurability / (double)_maxDurability;
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

    }
}
