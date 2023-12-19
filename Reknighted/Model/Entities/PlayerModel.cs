using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Reknighted.Controller.Collections;
using Reknighted.Controller;
using Reknighted.Model.Items;

namespace Reknighted.Model.Entities
{
    [JsonDerivedType(typeof(PlayerModel), "playerModel")]
    public class PlayerModel : IFightable, ITradeable
    {
        #region IPlayable

        private Location _location = Location.Hearts;
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
        private ItemModel?[] _equippedItems = new ItemModel[3] { null, null, null };
        [JsonPropertyName("items")]
        public List<ItemModel?> Items
        {
            get
            {
                return _items;
            }
        }
        private List<ItemModel?> _items = new List<ItemModel?>();
        private int _balance = 120;

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

        private int _maxHealth = 50;
        private int _currentHealth = 40;
        private int _fortune = 10;

        private int[] _defaultStats = new int[3] { 100, 0, 5 };
        [JsonIgnore]
        public List<ItemModel?> ItemReward { get => null; }
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonPropertyName("equipment")]
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
                double mp = 1;
                if (Artefact != null && Artefact.Buff == Buff.Health)
                {
                    mp = Artefact.Multiplier;
                }

                if (value < CurrentHealth)
                {
                    value = (int)(value / mp);
                }
                else
                {
                    value = (int)(value * mp);
                }


                if (value < MaxHealth)
                {
                    if (value >= 0)
                        _currentHealth = value;
                    else
                        _currentHealth = 1;
                }
                else
                {
                    _currentHealth = MaxHealth;
                }
            }
        }

        [JsonIgnore]
        public double HealthPercentage
        {
            get
            {

                return _currentHealth / (double)MaxHealth;
            }

            set
            {
                _currentHealth = (int)(value * MaxHealth);
            }

        }
        public Faction Faction { get; set; }
        public int BaseDamage { get; set; } = 1;
        public int BaseProtection { get; set; } = 1;

        [JsonIgnore]
        public int Damage
        {
            get
            {
                int value = BaseDamage;
                if (Weapon != null)
                {
                    value += Weapon.Damage;
                    if (Artefact != null && Artefact.Buff == Buff.Damage)
                    {
                        value = (int)(value * Artefact.Multiplier);
                    }
                    if (Faction == Faction.Spades)
                    {
                        value = (int)(value * 1.5);
                    }
                }
                return value;
            }
        }
        [JsonIgnore]
        public int Protection
        {
            get
            {
                int value = BaseProtection;
                if (Armor != null)
                {
                    value += Armor.Protection;
                    if (Artefact != null && Artefact.Buff == Buff.Protection)
                    {
                        value = (int)(value * Artefact.Multiplier);
                    }
                    if (Faction == Faction.Hearts)
                    {
                        value = (int)(value * 1.5);
                    }
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

        List<ItemModel?> IFightable.ItemReward { get { return null; } }

        #endregion

        public void AddItem(ItemModel? newItem, bool testMode = false)
        {
            int index = Items.IndexOf(null);
            Items[index] = newItem;
            if (testMode) return;
            Game.Update();
        }

        public void RemoveItem(ItemModel? item, bool testMode = false)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == item)
                {
                    Items[i] = null;
                }

            }

            for (int i = 0; i < EquippedItems.Count(); i++)
            {
                if (EquippedItems[i] == item)
                {
                    EquippedItems[i] = null;
                }
            }

            if (testMode) return;
            Game.Update();
        }

        static int MaxIventorySize = 27;

        public PlayerModel(Faction faction)
        {
            Faction = faction;
            if (faction == Faction.Diamonds)
            {
                Balance = 540;
            }
            for (int i = 0; i < MaxIventorySize; i++)
            {
                Items.Add(null);

            }
        }

        [JsonConstructor]
        public PlayerModel(Location location, List<ItemModel?> items, int balance, ItemModel[] equippedItems, int maxHealth,
            int currentHealth, Faction faction, int baseDamage, int baseProtection, int fortune)
        {
            _location = location;
            _items = items;
            _balance = balance;
            _equippedItems = equippedItems;
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
            Faction = faction;
            BaseDamage = baseDamage;
            BaseProtection = baseProtection;
            _fortune = fortune;

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
            try
            {
                int max = Items.Count;

                if (index >= 0 && index < max)
                {
                    return Items[index];
                }
                else if (index >= max && index < max + 3)
                {
                    return EquippedItems[index - max];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Game.Logger.Error("Cannot get item." + ex.Message);
                throw;
            }

        }

        private void SetItem(int index, ItemModel? item)
        {   
            try
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
            catch (Exception ex)
            {
                Game.Logger.Error("Cannot set item." + ex.Message);
                throw;
            }


        }



    }
}
