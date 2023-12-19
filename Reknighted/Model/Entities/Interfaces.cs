using System.Collections.Generic;
using Reknighted.Model.Items;

namespace Reknighted.Model.Entities
{
    public interface IFightable
    {
        public WeaponModel? Weapon { get; set; }
        public ArmorModel? Armor { get; set; }
        public ArtefactModel? Artefact { get; set; }

        public List<ItemModel?> ItemReward { get; }

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public double HealthPercentage { get; set; }

        public int BaseDamage { get; set; }
        public int BaseProtection { get; set; }

        public int Damage { get; }
        public int Protection { get; }
        public int Balance { get; set; }



    }

    public interface ITradeable
    {
        public List<ItemModel?> Items { get; }
        public int Balance { get; set; }

        public void AddItem(ItemModel item, bool testMode = false);

        public void RemoveItem(ItemModel item, bool testMode = false);
        public ItemModel? this[int index] { get; set; }
    }

    public interface IMappable
    {
        public System.Windows.Point Point { get; set; }

        public Location City { get; set; }
        public string PathToIcon { get; set; }
    }
}
