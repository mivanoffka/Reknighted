﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reknighted.Controller;

namespace Reknighted.Model
{
    public class PlayerModel : IFightable, ITradeable
    {
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
        private int _balance = 10000;

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

        public double HealthPercentage
        {
            get
            {   

                return (double)_currentHealth / (double)MaxHealth;
            }

            set
            {
                _currentHealth = (int)(value * MaxHealth);
            }

        }

        public Faction Faction { get; set; }
        public int BaseDamage { get; set; } = 1;
        public int BaseProtection { get; set; } = 1;

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

            for (int i = 0; i < EquippedItems.Count(); i++)
            {
                if (EquippedItems[i] == item)
                {
                    EquippedItems[i] = null;
                }
            }
            Game.Update();
        }

        static int MaxIventorySize = 27;

        public PlayerModel(Faction faction)
        {
            Faction = faction;
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
