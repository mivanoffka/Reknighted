using Reknighted.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Reknighted.Controller.Game;

namespace Reknighted.Model.Items
{
    public class ArtefactModel : DurableItem
    {
        protected Buff _buff;
        protected double _multiplier = 0;

        public Buff Buff
        {
            get => _buff;
        }
        public double Multiplier
        {
            get
            {
                double value = _multiplier;

                if (Game.PlayerModel.Faction == Faction.Clubs && IsPossessed)
                {
                    value *= 1.5;
                }

                return value;
            }
        }

        public override ItemModel Copy()
        {
            return new ArtefactModel(_name, _description, _price, _maxDurability, _multiplier, Buff, _pathToImage);
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
            MoveToCell(EquipmentCells[0]);
            Update();
        }
    }
}
