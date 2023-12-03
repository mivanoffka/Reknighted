using Reknighted.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class Fighter : IFightable, IMappable
    {
        public string Name { get; init; } = string.Empty;

        public ItemModel? Reward { get; init; }

        private ItemModel?[] _equippedItems = new ItemModel?[3];
        private int _maxHealth = 50;
        private int _currentHealth = 40;
        private int _fortune = 10;
        private int _balance = 1000;

        public int BaseDamage { get; set; } = 1;
        public int BaseProtection { get; set; } = 1;

        public string PathToIcon { get; set; } = string.Empty;
        public System.Windows.Point Point { get; set; } = new System.Windows.Point(0, 0);

        private readonly int[] _defaultStats = new int[3] { 100, 0, 5 };

        public WeaponModel? Weapon
        {
            get => (WeaponModel?)_equippedItems[0];
            set => _equippedItems[0] = value;
        }

        public ArmorModel? Armor
        {
            get => (ArmorModel?)_equippedItems[1];
            set => _equippedItems[1] = value;
        }

        public ArtefactModel? Artefact
        {
            get => (ArtefactModel?)_equippedItems[2];
            set => _equippedItems[2] = value;
        }

        public ItemModel?[] EquippedItems
        {
            get => _equippedItems; 
            set => _equippedItems = value;
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value < MaxHealth ? value : MaxHealth;
        }

        public double HealthPercentage
        {
            get => (double)_currentHealth / _maxHealth;
            set => _currentHealth = (int)(value * _maxHealth);
        }

        public int Damage
        {
            get
            {
                int value = BaseDamage;
                if (Weapon != null)
                {
                    value += 2 * Weapon.Damage;
                    if (Artefact != null && Artefact.Buff == Buff.Damage)
                    {
                        value = (int)(value * Artefact.Multiplier);
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
                    value += 2 * Armor.Protection;
                    if (Artefact != null && Artefact.Buff == Buff.Protection)
                    {
                        value = (int)(value * Artefact.Multiplier);
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

        public Fighter(string name, ItemModel[] equipment, ItemModel? reward, string pathToIcon, System.Windows.Point position)
        {
            PathToIcon = pathToIcon;
            Point = position;
            Name = name;

            if (reward != null) Reward = reward;

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


    }
}
