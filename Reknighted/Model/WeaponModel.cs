﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override ItemModel Copy()
        {
            return new WeaponModel(_name, _description, _price, _maxDurability, _damage, pathToImage);
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

            result += "\n\nУрон: " + _damage;
            result += "\nПрочность: " + Math.Round(DurabilityPercentage * 100) + "%";
            result += "\nЦена: " + _price;

            return result;
        }

        public override string Help()
        {
            return "Можно перенести в слот [ОРУЖИЕ]";
        }
    }
}
