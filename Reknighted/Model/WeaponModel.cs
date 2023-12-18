using Reknighted.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Reknighted.Model
{
    public class WeaponModel : DurableItem
    {
        private int _damage;

        public int Damage
        {
            get
            {
                return (int)(_damage * Math.Sqrt(DurabilityPercentage));
            }
        }

        public WeaponModel(string name, string description, int cost, int maxDurability, int damage, string imageSource) : base(name, description, cost, maxDurability, imageSource)
        {
            this._damage = damage;
        }
        public WeaponModel() { }

        [JsonConstructor]
        public WeaponModel(int damage, int maxDurability, int currentDurability, string pathToImage, string name,
            string description, int price, bool isPossessed, Cell cell)
            : base(maxDurability, currentDurability, pathToImage, name, description, price, isPossessed, cell)
        {
            _damage = damage;
            _maxDurability = maxDurability;
        }

        public override ItemModel Copy()
        {
            return new WeaponModel(_name, _description, _price, _maxDurability, _damage, _pathToImage);
        }

        public override void Use()
        {
            MoveToCell(Game.EquipmentCells[0]);
            Game.Update();
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += "[ " + Name + " ] ";

            string editedDescription = "\n\n";

            int maxCounter = 30;
            int counter = 0;
            for (int i = 0; i < Description.Length; i++)
            {
                if (counter >= maxCounter && Description[i] == ' ')
                {
                    editedDescription += "\n";
                    counter = 0;
                }
                else
                {
                    editedDescription += Description[i];
                }
                counter++;

            }

            result += editedDescription;

            result += $"\n\n{Game.app.FindResource("lbDamage")}: " + _damage;
            result += $"\n{Game.app.FindResource("lbDurability")}: " + Math.Round(DurabilityPercentage * 100) + "%";
            result += $"\n{Game.app.FindResource("lbPrice")}: " + _price;

            return result;
        }

        public override string Help()
        {
            return $"{Game.app.FindResource("weaponHint")}";
        }
    }
}
