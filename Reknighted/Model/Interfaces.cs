using System.Collections.Generic;

namespace Reknighted.Model
{
    public interface IFightable
    {
        public ItemModel Weapon { get; set; }
        public ItemModel Armor { get; set; }
        public ItemModel Artefact { get; set; }

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public double HealthPercentage { get; set; }

        public int Damage { get; }
        public int Protection { get; }
        public int Fortune { get; set; }





        public List<Effects> Effects { get; set; }

    }

    public interface ITradeable
    {
        public List<ItemModel?> Items { get; }
        public int Balance { get; set; }

        public void AddItem(ItemModel item);

        public void RemoveItem(ItemModel item);
        public ItemModel? this[int index] {
            get;
            set;
        }


    }

    public interface IPlayable
    {
        public Location Location { get; set; }

    }
}
