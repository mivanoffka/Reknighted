using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reknighted.Controller;

namespace Reknighted.Model
{
    public class PlayerModel : IFightable, ITradeable, IPlayable
    {
        public static PlayerModel DefaultPlayerModel = new PlayerModel();

        #region IPlayable

        private City _location = City.Masquarade;

        public City Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

        #endregion

        #region ITradeable

        private List<ItemModel?> _items = new List<ItemModel?>();
        private int _balance = 100;

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
                //Game.ResetAndUpdate();
            }
        }

        #endregion

        #region IFightable

        private ItemModel?[] _equippedItems = new ItemModel[3] { null, null, null };
        private int _maxHealth = 50;
        private int _currentHealth = 40;
        private int _fortune =  10;

        private int[] _defaultStats = new int[3] { 100, 0, 5 };
        private List<Effects> _effects = new List<Effects> { };

        public ItemModel? Reward { get => null; }

        public WeaponModel? Weapon
        {
            get
            {
                return (WeaponModel?)_equippedItems[0];
            }

            set
            {
                _equippedItems[0] = value;
            }
        }
        public ArmorModel? Armor
        {
            get
            {
                return (ArmorModel?)_equippedItems[1];
            }

            set
            {
                _equippedItems[1] = value;
            }
        }
        public ArtefactModel? Artefact
        {
            get
            {
                return (ArtefactModel?)_equippedItems[2];
            }

            set
            {
                _equippedItems[2] = value;
            }
        }
        public ItemModel?[] EquippedItems
        {
            get
            {
                return _equippedItems;
            }
            set
            {
                _equippedItems = value;
            }
        }



        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }

            set
            {
                _maxHealth = value;
            }
        }

        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }

            set
            {
                if (value < MaxHealth)
                {   
                    if (value >= 0.2 *  MaxHealth)
                        _currentHealth = value;
                    else 
                        _currentHealth = (int)(0.2 * MaxHealth);
                }
                else
                {
                    _currentHealth = MaxHealth;
                }
            }
        }

        public double HealthPercentage
        {
            get
            {
                return (double)_currentHealth / (double)_maxHealth;
            }

            set
            {
                _currentHealth = (int)(value * _maxHealth);
            }

        }

        public int Damage
        {
            get
            {
                int value = 5;
                if (Weapon != null)
                {
                    value += ((WeaponModel)Weapon).Damage; 
                }
                return value;
            }
        }
        public int Protection
        {
            get
            {
                int value = 5;

                double mp = 1;
                if (Artefact != null && ((ArtefactModel)Artefact)?.Buff == Buff.Protection)
                {
                    mp = ((ArtefactModel)Artefact).Multiplier;
                }

                if (Armor != null)
                {
                    value += (int)(((ArmorModel)Armor).Protection * mp);
                }
                return value;
            }
        }

        public int Fortune
        {
            get
            {
                return _fortune;
            }

            set
            {
                _fortune = value;
            }
        }

        public List<Effects> Effects { get; set; }

        #endregion

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

        public void SellTo(ITradeable trader, ItemModel item)
        {
            throw new NotImplementedException();
        }

        public void BuyFrom(ITradeable trader, ItemModel item)
        {
            throw new NotImplementedException();
        }

        static int MaxIventorySize = 27;

        public PlayerModel()
        {
            for (int i = 0; i < MaxIventorySize; i++)
            {
                this.Items.Add(null);

            }
        }

        public ItemModel? this[int index]
        {
            get
            {
                return GetItem(index);
            }

            set
            {
                SetItem(index, value);
            }
        }


        

        private ItemModel? GetItem(int index)
        {
            int max = Items.Count;

            if (index >= 0 && index < max)
            {
                return Items[index];
            }
            else if (index >= max && index < max + 3)
            {
                return EquippedItems[index-max];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void SetItem(int index, ItemModel? item)
        {
            int max = Items.Count;

            if (index >= 0 && index < max)
            {
                Items[index] = item;
            }
            else if (index >= max && index < max + 3)
            {
                EquippedItems[index - max] = item;
                Game.PlayerView.UpdateStats();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }



    }
}
