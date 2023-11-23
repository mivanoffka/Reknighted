using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    internal class ArmorModel : ItemModel
    {
        private int _protection;

        public int Protection
        {
            get
            {
                return _protection;
            }
        }

        public ArmorModel(string name, string description, int cost, int protection, string imageSource) : base(name, description, cost, imageSource)
        {
            this._protection = protection;
        }

        public ArmorModel(ArmorModel armorModel)
        {
            this._name = armorModel.Name;
            this._description = armorModel.Description;
            this._price = armorModel.Price;
            this._protection = armorModel.Protection;
            this._image = armorModel.Image;

        }

        public override string Information()
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
            result += "\nЦена: " + _price;


            return result;
        }
    }
}
