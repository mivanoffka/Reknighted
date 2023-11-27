using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class ArtefactModel : ItemModel
    {
        protected Buff? _buff = null; 
        protected double _multiplier = 0;

        public Buff? Buff
        {
            get => this._buff;
        }
        public double Multiplier
        {
            get => _multiplier;
        }

        public ArtefactModel(string name, string description, int cost, Buff buff, double multiplier, string imageSource) : base(name, description, cost, imageSource)
        {
            _buff = buff;
            _multiplier = multiplier;
        }

        public ArtefactModel(ArtefactModel model)
        {
            _name = model.Name;
            _description = model.Description;
            _price = model.Price;
            _image = model.Image;
            _buff = model.Buff;
            _multiplier = model.Multiplier;

        }

        public override void Use()
        {
        }
    }
}
