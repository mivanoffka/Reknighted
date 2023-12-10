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

        private static ItemModel[] patricks = new ItemModel[]
        {
            Items.Food["eggs"], Items.Food["chicken"], Items.Food["eggs"], Items.Food["eggs"], Items.Food["bacon"], Items.Food["chicken"],
            Items.Food["eggs"], Items.Food["chicken"], Items.Food["bacon"], Items.Food["bacon"], Items.Food["eggs"], Items.Food["eggs"],
            Items.Food["chicken"], Items.Food["eggs"], Items.Food["bacon"], Items.Food["eggs"], Items.Food["eggs"], Items.Food["chicken"]

        };

        private static ItemModel[] peters = new ItemModel[]
        {
            Items.Food["apple_green"], Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["cherry"], Items.Food["strawberry"], Items.Food["lemon"],
            Items.Food["apple_green"], Items.Food["cherry"], Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["peach"], Items.Food["apple_green"],
            Items.Food["apple_green"], Items.Food["peach"], Items.Food["strawberry"], Items.Food["cherry"], Items.Food["apple_red"], Items.Food["apple_green"], Items.Food["apple_green"],
            Items.Food["lemon"], Items.Food["strawberry"]
        };

        private static ItemModel[] beniamins = new ItemModel[]
        {
            Items.Armors["leather_hat"], Items.Armors["cross_helmet"], Items.Armors["generic_helmet"],
            Items.Armors["leather_hat"], Items.Armors["generic_helmet"], Items.Armors["leather_hat"],
            Items.Armors["regular_helmet"], Items.Armors["leather_hat"], Items.Armors["generic_helmet"],
            Items.Armors["leather_hat"], Items.Armors["regular_helmet"], Items.Armors["leather_hat"],
            Items.Armors["regular_helmet"], Items.Armors["leather_hat"], Items.Armors["generic_helmet"],
            Items.Armors["cross_helmet"], Items.Armors["horned_helmet"], Items.Armors["leather_hat"],
        };

        private static ItemModel[] johans = new ItemModel[]
        {
            Items.Weapons["veteran_sword"], Items.Weapons["generic_sword"], Items.Weapons["generic_spear"],
            Items.Weapons["veteran_sword"], Items.Weapons["generic_club"], Items.Weapons["regular_sword"],
            Items.Weapons["generic_spear"], Items.Weapons["generic_sword"], Items.Weapons["generic_club"],
            Items.Weapons["generic_sword"], Items.Weapons["generic_club"], Items.Weapons["regular_sword"],
            Items.Weapons["two_handed_sword"], Items.Weapons["generic_spear"], Items.Weapons["veteran_sword"],
            Items.Weapons["two_handed_sword"], Items.Weapons["generic_club"], Items.Weapons["regular_sword"]
        };

        public static Dictionary<string, TraderModel> AllTradersDefaultState = new()
        {
            {"food", new TraderModel(TraderType.Food, "Еда", Items.Food.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(10, 10), Location.ShowRoom)},
            {"weapons", new TraderModel(TraderType.Weapon, "Оружие", Items.Weapons.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(60, 10), Location.ShowRoom)},
            {"armor", new TraderModel(TraderType.Armor, "Броня", Items.Armors.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(110, 10), Location.ShowRoom)},
            {"artefacts", new TraderModel(TraderType.Artefact, "Артефакты", Items.Artefacts.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(160, 10), Location.ShowRoom)},
            {"potions", new TraderModel(TraderType.Universal, "Зелья", Items.Potions.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(210, 10), Location.ShowRoom)},


            // Червы
            {"peter", new TraderModel(TraderType.Food, "Садовод Пётр", peters, Items.PathTo("green_houses"), new System.Windows.Point(60, 10), Location.Hearts)},
            {"patrick", new TraderModel(TraderType.Food, "Мясник Патрик", patricks, Items.PathTo("three_houses"), new System.Windows.Point(100, 60), Location.Hearts)},
            {"beniamin", new TraderModel(TraderType.Armor, "Бронник Вениамин", beniamins, Items.PathTo("chimney_house"), new System.Windows.Point(200, 140), Location.Hearts)},
            {"johans", new TraderModel(TraderType.Weapon, "Оружейник Иоанн", johans, Items.PathTo("tiny_house"), new System.Windows.Point(130, 180), Location.Hearts)}
        };

        public static Dictionary<string, Fighter> AllFightersDefaultState = new()
        {
            {"example", new Fighter("example", new ItemModel?[3], null, Items.PathTo("red_tower"), new System.Windows.Point(10, 60), Location.ShowRoom)},

            // Червы
            {"robber_1", new Fighter("Разбойник", new ItemModel?[3] {Items.Weapons["generic_club"], Items.Armors["leather_hat"],  null }, Items.Weapons["generic_club"], Items.PathTo("ruined_tower"), new System.Windows.Point(250, 60), Location.Hearts) },
            {"robber_2", new Fighter("Разбойник", new ItemModel?[3] {Items.Weapons["generic_sword"], null,  null}, Items.Weapons["generic_sword"], Items.PathTo("ruined_tower"), new System.Windows.Point(90, 160), Location.Hearts) },
            {"robber_3", new Fighter("Разбойник", new ItemModel?[3] {Items.Weapons["generic_spear"], null,  null}, Items.Weapons["generic_spear"], Items.PathTo("ruined_tower"), new System.Windows.Point(190, 180), Location.Hearts) }


        };

        public static void Initialize()
        {
 
        }
    }
}
