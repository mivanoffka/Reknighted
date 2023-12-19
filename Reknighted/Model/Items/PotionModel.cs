using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Reknighted.Controller;

namespace Reknighted.Model.Items
{
    internal class PotionModel : ItemModel
    {
        private int _addition;
        private Buff _buff;

        public int Addition
        {
            get
            {
                int value = _addition;

                if (Game.PlayerModel.Faction == Faction.Clubs && IsPossessed)
                {
                    value = (int)(value * 1.5);
                }

                return _addition;
            }
        }

        public Buff Buff { get { return _buff; } }

        public PotionModel(string name, string description, int cost, int addition, Buff buff, string imageSource) : base(name, description, cost, imageSource)
        {
            _addition = addition;
            _buff = buff;
        }
        public PotionModel() { }

        [JsonConstructor]
        public PotionModel(int addition, Buff buff, string pathToImage, string name, string description, int price,
            bool isPossessed, Cell cell) : base(name, description, price, isPossessed, cell, pathToImage)
        {
            _addition = addition;
            _buff = buff;
        }

        public override ItemModel Copy()
        {
            return new PotionModel(_name, _description, _price, _addition, _buff, _pathToImage);
        }

        public override void Use()
        {
            if (Buff == Buff.Health)
            {
                Game.PlayerModel!.MaxHealth += Addition;
            }
            if (Buff == Buff.Protection)
            {
                Game.PlayerModel!.BaseProtection += Addition;
            }
            if (Buff == Buff.Damage)
            {
                Game.PlayerModel!.BaseDamage += Addition;
            }

            Game.PlayerModel!.RemoveItem(this);
        }
    }
}
