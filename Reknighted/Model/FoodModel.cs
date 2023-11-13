using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    internal class WeaponModel : ItemModel
    {
        private int _damage;

        public int Damage
        {
            get
            {
                return _damage;
            }
        }

        public WeaponModel(string name, string description, int cost, int damage, string imageSource) : base(name, description, cost, imageSource)
        {
            this._damage = damage;
        }

        public WeaponModel(WeaponModel weaponModel)
        {   
            this._name = weaponModel.Name;
            this._description = weaponModel.Description;
            this._basePrice = weaponModel.BasePrice;
            this._damage = weaponModel.Damage;
            this._image = weaponModel.Image;

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

            result += "\n\nУрон: " + _damage;
            result += "\nЦена: " + _basePrice;

            return result;
        }
    }
}
