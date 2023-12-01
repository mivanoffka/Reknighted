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
                return _addition;
            }
        }

        public Buff Buff { get { return _buff; } }

        public PotionModel(string name, string description, int cost, int addition, string imageSource, Buff buff) : base(name, description, cost, imageSource)
        {
            this._addition = addition;
            this._buff = buff;
        }

        public PotionModel(PotionModel model)
        {
            this._name = model.Name;
            this._description = model.Description;
            this._price = model.Price;
            this._addition = model.Addition;
            this._buff = model.Buff;
            this._image = model.Image;
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
