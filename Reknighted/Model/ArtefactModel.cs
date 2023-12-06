using Reknighted.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Reknighted.Controller.Game;

namespace Reknighted.Model
{
    public class ArtefactModel : DurableItem
    {
        protected Buff _buff;
        protected double _multiplier = 0;

        public Buff Buff
        {
            get => this._buff;
        }
        public double Multiplier
        {
            get
            {
                double value = this._multiplier;

                if (Game.PlayerModel.Faction == Faction.Clubs && IsPossessed)
                {
                    value *= 1.5;
                }

                return value;
            }
        }

        public override ItemModel Copy()
        {
            return new ArtefactModel(this._name, this._description, this._price, this._maxDurability, this._multiplier, this.Buff, _pathToImage);
        }

        public ArtefactModel(string name, string description, int cost, int durability, double multiplier, Buff buff, string imageSource) : base(name, description, cost, durability, imageSource)
        {
            _buff = buff;
            _multiplier = multiplier;
        }
        public ArtefactModel() { }

        [JsonConstructor]
        public ArtefactModel(Buff buff, double multiplier, int maxDurability, int currentDurability,
            string pathToImage, string name, string description, int price, bool isPossessed, Cell cell) :
            base(maxDurability, currentDurability, pathToImage, name, description, price, isPossessed, cell)
        {
            _buff = buff;
            _multiplier = multiplier;
        }

        public override void Use()
        {
            MoveToCell(Game.EquipmentCells[0]);
            Game.Update();
        }
    }
}
