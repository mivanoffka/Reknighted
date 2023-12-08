using Reknighted.Model;
using Reknighted.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Collections
{
    public class Entities
    {
        public static Dictionary<string, MapIcon> GlobalMap = new()
        {
            {"showroom", new MapIcon("Шоурум", Location.ShowRoom, Items.PathTo("red_apple"), new System.Windows.Point(50, 50))},
            {"hearts", new MapIcon("Червы", Location.Hearts, Items.PathTo("hearts"), new System.Windows.Point(10, 10))},
            {"clubs", new MapIcon("Трефы", Location.Clubs, Items.PathTo("clubs"), new System.Windows.Point(60, 10))},
            {"diamonds", new MapIcon("Бубны", Location.Diamonds, Items.PathTo("diamonds"), new System.Windows.Point(110, 10))},
            {"spades", new MapIcon("Пики", Location.Spades, Items.PathTo("spades"), new System.Windows.Point(160, 10))}
        };


        private static ItemModel[] peters = new ItemModel[]
        {
            Items.Food["apple_green"], Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["cherry"], Items.Food["strawberry"], Items.Food["lemon"],
            Items.Food["apple_green"], Items.Food["cherry"], Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["peach"], Items.Food["apple_green"],
            Items.Food["apple_green"], Items.Food["peach"], Items.Food["strawberry"], Items.Food["cherry"], Items.Food["apple_red"], Items.Food["apple_green"], Items.Food["apple_green"],
            Items.Food["lemon"], Items.Food["strawberry"]
        };

        public static Dictionary<string, TraderModel> AllTradersDefaultState = new()
        {
            {"food", new TraderModel(TraderType.Food, "Еда", Items.Food.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(10, 10), Location.ShowRoom)},
            {"weapons", new TraderModel(TraderType.Weapon, "Оружие", Items.Weapons.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(60, 10), Location.ShowRoom)},
            {"armor", new TraderModel(TraderType.Armor, "Броня", Items.Armors.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(110, 10), Location.ShowRoom)},
            {"artefacts", new TraderModel(TraderType.Artefact, "Артефакты", Items.Artefacts.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(160, 10), Location.ShowRoom)},
            {"potions", new TraderModel(TraderType.Universal, "Зелья", Items.Potions.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(210, 10), Location.ShowRoom)},

            {"peter", new TraderModel(TraderType.Food, "Садовод Пётр", peters, Items.PathTo("green_houses"), new System.Windows.Point(60, 10), Location.Hearts)}
        };

        public static Dictionary<string, Fighter> AllFightersDefaultState = new()
        {
            {"example", new Fighter("example", new ItemModel?[3], null, Items.PathTo("red_tower"), new System.Windows.Point(10, 60), Location.ShowRoom) }
        };

        public static void Initialize()
        {
 
        }
    }
}
