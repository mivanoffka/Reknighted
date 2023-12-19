using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Reknighted.Model.Entities;

namespace Reknighted.Controller.Serialization
{
    [JsonDerivedType(typeof(ObjectsState))]
    public class ObjectsState
    {
        [JsonInclude]
        public PlayerModel Player;
        [JsonInclude]
        public List<TraderModel> Traders;
        [JsonInclude]
        public List<Fighter> Fighters;

        public ObjectsState() { }
        [JsonConstructor]
        public ObjectsState(PlayerModel player, List<TraderModel> traders, List<Fighter> fighters)
        {
            Player = player;
            Traders = traders;
            Fighters = fighters;
        }
    }
}
