using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Reknighted.Controller;

namespace Reknighted.Model
{
    internal class FoodModel : ItemModel
    {
        private int _satiety;

        public int Satiety
        {
            get
            {
                return _satiety;
            }
        }

        public FoodModel(string name, string description, int cost, int satiety, string imageSource) : base(name, description, cost, imageSource)
        {
            this._satiety = satiety;
        }

        [JsonConstructor]
        public FoodModel(string name, string description, int price, bool isPossessed, Cell cell, string pathToImage, int satiety) :
            base(name,description,price, isPossessed, cell, pathToImage)
        {
            _satiety = satiety;
        }


        public override ItemModel Copy()
        {
            return new FoodModel(this._name, this._description, this._price, this._satiety, _pathToImage);
        }

        public FoodModel(FoodModel foodModel)
        {   
            this._name = foodModel.Name;
            this._description = foodModel.Description;
            this._price = foodModel.Price;
            this._satiety = foodModel.Satiety;
            this._image = foodModel.Image;

        }

        public override string ToString()
        {
            string result = string.Empty;
            //result += "[ " + Name + " ] ";

            string editedDescription = "";

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

            result += $"\n\n{Game.app.FindResource("lbSatiety")}: " + _satiety;
            result += $"\n {Game.app.FindResource("lbPrice")}: " + _price;

            return result;
        }

        public override string Help()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"{Game.app.FindResource("foodHint")}");

            return result.ToString();
        }

        public override void Use()
        {
            Game.PlayerModel!.CurrentHealth += this.Satiety;
            Game.PlayerModel!.RemoveItem(this);
        }
    }
}
