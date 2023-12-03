using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reknighted.Controller;

namespace Reknighted.Model
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
            this._addition = addition;
            this._buff = buff;
        }

        public override ItemModel Copy()
        {
            return new PotionModel(this._name, this._description, this._price, this._addition, _buff, pathToImage);
        }

        public override void Use()
        {
            if (Buff == Buff.Health)
            {
                Game.PlayerModel!.MaxHealth += this.Addition;
            }
            if (Buff == Buff.Protection)
            {
                Game.PlayerModel!.BaseProtection += this.Addition;
            }
            if (Buff == Buff.Damage)
            {
                Game.PlayerModel!.BaseDamage += this.Addition;
            }

            Game.PlayerModel!.RemoveItem(this);
        }
    }
}
