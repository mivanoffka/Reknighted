using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
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
