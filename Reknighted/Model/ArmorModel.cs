using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reknighted.Controller;

namespace Reknighted.Model
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
            this._protection = protection;
        }

        public override ItemModel Copy()
        {
            return new ArmorModel(_name, _description, _price, _maxDurability, _protection, pathToImage);
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

            result += "\n\nЗащита: " + _protection;
            result += "\nПрочность: " + Math.Round(DurabilityPercentage * 100) + "%";
            result += "\nЦена: " + _price;


            return result;
        }

        public override string Help()
        {
            return "Можно перенести в слот [БРОНЯ]";
        }
    }
}
