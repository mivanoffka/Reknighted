using Reknighted.Controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    [JsonDerivedType(typeof(Fighter))]
    public class Fighter : IFightable, IMappable
    {

        public Location City { get; set; }
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
        
        [JsonIgnore]
        public WeaponModel? Weapon
        {
            get => (WeaponModel?)_equippedItems[0];
            set => _equippedItems[0] = value;
        }
        
        [JsonIgnore]
        public ArmorModel? Armor
        {
            get => (ArmorModel?)_equippedItems[1];
            set => _equippedItems[1] = value;
        }

        [JsonIgnore]
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

        [JsonIgnore]
        public double HealthPercentage
        {
            get => (double)_currentHealth / _maxHealth;
            set => _currentHealth = (int)(value * _maxHealth);
        }
        [JsonIgnore]
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
        [JsonIgnore]
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



        public Fighter(string name, ItemModel[] equipment, ItemModel? reward, string pathToIcon, System.Windows.Point position, Location city)
        {
            City = city;
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

        [JsonConstructor]
        public Fighter(Location city, string name, ItemModel reward, int baseDamage, int baseProtection, string pathToIcon, System.Windows.Point point, ItemModel[] equippedItems, int maxHealth, int currentHealth, int damage, int fortune, int balance)
        {
            City = city;
            Name = name;
            Reward = reward;
            BaseDamage = baseDamage;
            BaseProtection = baseProtection;
            PathToIcon = pathToIcon;
            Point = point;
            _equippedItems = equippedItems;
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
            _fortune = fortune;
            _balance = balance;
        }
    }
}
