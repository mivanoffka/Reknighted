using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Reknighted.Controller;

namespace Reknighted.Model.Items
{
    public class ArmorModel : DurableItem
    {
        private int _protection;

        public int Protection
        {
            get
            {
                return (int)(_protection * Math.Sqrt(DurabilityPercentage));
            }
        }

        public ArmorModel(string name, string description, int cost, int maxDurability, int protection, string imageSource) : base(name, description, cost, maxDurability, imageSource)
        {
            _protection = protection;
        }
        public ArmorModel() { }

        [JsonConstructor]
        public ArmorModel(int protection, int maxDurability, int currentDurability,
            string pathToImage, string name, string description, int price, bool isPossessed, Cell cell)
            : base(maxDurability, currentDurability, pathToImage, name, description, price, isPossessed, cell)
        {
            _protection = protection;
        }

        public override ItemModel Copy()
        {
            return new ArmorModel(_name, _description, _price, _maxDurability, _protection, _pathToImage);
        }

        public override void Use()
        {
            MoveToCell(Game.EquipmentCells[1]);
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

            result += $"\n\n{Game.app.FindResource("lbProtection")}: " + _protection;
            result += $"\n{Game.app.FindResource("lbDurability")}: " + Math.Round(DurabilityPercentage * 100) + "%";
            result += $"\n{Game.app.FindResource("lbPrice")}: " + _price;

            return result;
        }

        public override string Help()
        {
            return $"{Game.app.FindResource("armorHint")}";
        }
    }
}
