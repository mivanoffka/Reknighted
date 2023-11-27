using Reknighted.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class Fighter : IFightable
    {
        private string _name = "";
        public string Name { get { return _name; } }


        private ItemModel?[] _equippedItems = new ItemModel[3] { null, null, null };
        private int _maxHealth = 50;
        private int _currentHealth = 40;
        private int _fortune = 10;
        private int _balance = 1000;

        private int[] _defaultStats = new int[3] { 100, 0, 5 };
        private List<Effects> _effects = new List<Effects> { };

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
                    _currentHealth = value;
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
            }
        }

        public Fighter(string name, ItemModel[] equipment = null)
        {
            this._name = name;

            if (equipment != null)
            {   
                try
                {
                    Weapon = (WeaponModel)equipment[0];
                    Armor = (ArmorModel)equipment[1];
                    Artefact = (ArtefactModel)equipment[2];
                }
                catch
                {
                    Game.Error("Некорректный массив экипировки для " + name);
                }

            }

        }

        public List<Effects> Effects { get; set; }

    }
}
