using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Reknighted.Model
{
    public enum Location
    {
        Masquarade, Hearts, Clubs, Trefles, Diamonds
    }
    
    public enum Effects
    {

    }

    public enum TraderType
    {
        Universal, Food, Armor, Weapon, Artefact
    }

    public enum Family
    {
        Hearts, Clubs, Trefles, Diamonds
    }


    public interface IFightable
    {
        public ItemModel Weapon { get; set; }
        public ItemModel Armor { get; set; }
        public ItemModel Artefact { get; set; }

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Protection { get; set; }


        public List<Effects> Effects { get; set; }

    }

    public interface ITradeable
    {
        public List<ItemModel?> Items { get; }
        public int Balance { get; set; }

    }

    public interface IPlayable
    {
        public Location Location { get; set; }

    }


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
                if (value < 0) {
                    value = 0;
                }

                _balance = value;
                Game.ResetAndUpdate();
            }
        }

        #endregion

        #region IFightable


        private ItemModel?[] _equippedItems = new ItemModel[3] { null, null, null };
        private int[] _stats = new int[3] { 0, 0, 0 };
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

        public void GiveItem(ItemModel? newItem)
        {
            int index = Items.IndexOf(null);
            Items[index] = newItem;
            Game.ResetAndUpdate();
        }

        public void UpdateStats()
        {
            Protection = 10;
            Damage = 5;

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

            }
            catch
            {

            }

        }

        static int MaxIventorySize = 27;

        public PlayerModel()
        {
            for (int i = 0; i < MaxIventorySize; i++)
            {
                this.Items.Add(null);

            }
        }

    }

}
