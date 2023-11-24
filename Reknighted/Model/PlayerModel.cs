using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class PlayerModel : IFightable, ITradeable, IPlayable
    {
        public static PlayerModel DefaultPlayerModel = new PlayerModel();

        #region IPlayable

        private Location _location = Location.Masquarade;

        public Location Location
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
                Game.ResetAndUpdate();
            }
        }

        #endregion

        #region IFightable


        private ItemModel?[] _equippedItems = new ItemModel[3] { null, null, null };
        private int[] _stats = new int[3] { 100, 0, 0 };
        private int[] _defaultStats = new int[3] { 100, 0, 5 };
        private List<Effects> _effects = new List<Effects> { };

        public ItemModel? Weapon
        {
            get
            {
                return _equippedItems[0];
            }

            set
            {
                _equippedItems[0] = value;
            }
        }
        public ItemModel? Armor
        {
            get
            {
                return _equippedItems[1];
            }

            set
            {
                _equippedItems[1] = value;
            }
        }
        public ItemModel? Artefact
        {
            get
            {
                return _equippedItems[2];
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

        public int Health
        {
            get
            {
                return _stats[0];
            }

            set
            {
                _stats[0] = value;
            }
        }
        public int Damage
        {
            get
            {
                return _stats[2];
            }

            set
            {
                _stats[2] = value;
            }
        }
        public int Protection
        {
            get
            {
                return _stats[1];
            }

            set
            {
                _stats[1] = value;
            }
        }

        public List<Effects> Effects { get; set; }

        #endregion

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

        public void UpdateStats()
        {
            Protection = _defaultStats[1];
            Damage = _defaultStats[2];

            try
            {
                if (Armor != null)
                {
                    ArmorModel armorModel = (ArmorModel)Armor;
                    Protection += armorModel.Protection;
                }

                if (Weapon != null)
                {
                    WeaponModel weaponModel = (WeaponModel)Weapon;
                    Damage += weaponModel.Damage;
                }


                if (Game.damageLabel != null)
                {
                    Game.damageLabel.Content = Damage.ToString();
                }

                if (Game.protectionLabel != null)
                {
                    Game.protectionLabel.Content = Protection.ToString();
                }

                if (Game.healthLabel != null)
                {
                    Game.healthLabel.Content = Health.ToString();
                }

            }
            catch
            {

            }

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
                UpdateStats();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
