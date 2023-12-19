using Reknighted.Model;
using Reknighted.Model.Entities;
using Reknighted.Model.Items;
using Reknighted.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Windows.Point;


namespace Reknighted.Controller.Collections
{
    public class Entities
    {
        static Application app = Application.Current;
        #region Инвентари по умолчанию всех торговцев

        private static ItemModel[] patricks = new ItemModel[]
{
            Items.Food["eggs"], Items.Food["chicken"], Items.Food["eggs"], Items.Food["eggs"], Items.Food["bacon"], Items.Food["chicken"],
            Items.Food["eggs"], Items.Food["chicken"], Items.Food["bacon"], Items.Food["bacon"], Items.Food["eggs"], Items.Food["eggs"],
            Items.Food["chicken"], Items.Food["eggs"], Items.Food["bacon"], Items.Food["eggs"], Items.Food["eggs"], Items.Food["chicken"]

};

        private static ItemModel[] peters = new ItemModel[]
        {
            Items.Food["apple_red"], Items.Food["apple_green"], Items.Food["apple_red"],
            Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["apple_red"],
            Items.Food["apple_green"], Items.Food["apple_green"], Items.Food["apple_red"],
            Items.Food["apple_green"], Items.Food["apple_green"], Items.Food["apple_red"],
            Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["apple_green"],
            Items.Food["apple_red"], Items.Food["apple_green"], Items.Food["apple_red"],
        };

        private static ItemModel[] beniamins = new ItemModel[]
        {
            Items.Armors["leather_hat"], Items.Armors["generic_helmet"], Items.Armors["leather_hat"],
            Items.Armors["regular_helmet"], Items.Armors["cross_helmet"], Items.Armors["generic_helmet"],
            Items.Armors["leather_hat"], Items.Armors["regular_helmet"], Items.Armors["horned_helmet"],
        };

        private static ItemModel[] johans = new ItemModel[]
        {
            Items.Weapons["veteran_sword"], Items.Weapons["generic_sword"], Items.Weapons["generic_spear"],
            Items.Weapons["generic_spear"], Items.Weapons["generic_sword"], Items.Weapons["generic_club"],
            Items.Weapons["two_handed_sword"], Items.Weapons["generic_club"], Items.Weapons["regular_sword"]
        };

        private static ItemModel[] alexanders = new ItemModel[]
        {
            Items.Food["cheese"], Items.Food["steak"], Items.Food["cheese"],
            Items.Food["steak"], Items.Food["cheese"], Items.Food["steak"],
            Items.Food["steak"], Items.Food["cheese"], Items.Food["cheese"]
        };

        private static ItemModel[] beatrices = new ItemModel[]
        {
            Items.Food["jam"], Items.Food["honey"], Items.Food["cookie"],
            Items.Food["candy"], Items.Food["cookie"], Items.Food["jam"],
            Items.Food["cookie"], Items.Food["pretzel"], Items.Food["waffles"],
            Items.Food["candy"], Items.Food["waffles"], Items.Food["candy"],
            Items.Food["pretzel"], Items.Food["waffles"], Items.Food["honey"],
            Items.Food["candy"], Items.Food["jam"], Items.Food["cookie"]
        };

        private static ItemModel[] franks = new ItemModel[]
        {
            Items.Weapons["generic_spear"], Items.Weapons["generic_spear"], Items.Weapons["generic_club"],
            Items.Weapons["veteran_sword"], Items.Weapons["fencing_sword"], Items.Weapons["generic_sword"]
        };

        private static ItemModel[] edwards = new ItemModel[]
        {
            Items.Armors["leather_hat"], Items.Armors["cross_helmet"], Items.Armors["horned_helmet"],
            Items.Armors["leather_hat"], Items.Armors["guardian_helmet"], Items.Armors["leather_hat"],
            Items.Armors["regular_helmet"]
        };

        private static ItemModel[] joshuas = new ItemModel[]
        {
            Items.Food["pepperoni"], Items.Food["pepperoni"], Items.Food["chicken"],
            Items.Food["pepperoni"], Items.Food["bacon"], Items.Food["steak"], Items.Food["pepperoni"]
        };

        private static ItemModel[] benedicts = new ItemModel[]
        {
            Items.Artefacts["ruby_ring"], Items.Artefacts["sapphire_ring"], Items.Artefacts["emerald_ring"], Items.Artefacts["amethyst_ring"],
            Items.Artefacts["ruby_necklace"], Items.Artefacts["sapphire_necklace"], Items.Artefacts["emerald_necklace"], Items.Artefacts["amethyst_necklace"]
        };

        private static ItemModel[] daniels = new ItemModel[]
        {
            Items.Food["watermelon"], Items.Food["cherry"], Items.Food["strawberry"], Items.Food["lemon"],
            Items.Food["cherry"], Items.Food["watermelon"], Items.Food["peach"], Items.Food["peach"],
            Items.Food["strawberry"], Items.Food["cherry"], Items.Food["watermelon"],
            Items.Food["lemon"], Items.Food["strawberry"], Items.Food["watermelon"],
        };

        private static ItemModel[] walters = new ItemModel[]
        {
            Items.Weapons["veteran_sword"], Items.Weapons["two_handed_sword"], Items.Weapons["generic_spear"], Items.Weapons["fencing_sword"],
            Items.Weapons["generic_club"], Items.Weapons["fencing_sword"]
        };

        private static ItemModel[] eugenes = new ItemModel[]
        {
            Items.Armors["generic_helmet"], Items.Armors["regular_helmet"], Items.Armors["cross_helmet"], Items.Armors["elite_helmet"],
            Items.Armors["regular_helmet"], Items.Armors["guardian_helmet"], Items.Armors["horned_helmet"]
        };

        private static ItemModel[] lukas = new ItemModel[]
        {
            Items.Food["pickle"], Items.Food["tomato"], Items.Food["onion"],
            Items.Food["tomato"], Items.Food["potato"], Items.Food["pepper_green"],
            Items.Food["pickle"], Items.Food["onion"], Items.Food["pepper_green"],
            Items.Food["tomato"], Items.Food["potato"], Items.Food["pepper_red"],
            Items.Food["onion"], Items.Food["pepper_green"], Items.Food["pepper_red"],
            Items.Food["pickle"], Items.Food["potato"], Items.Food["tomato"]
        };

        private static ItemModel[] arthurs = new ItemModel[]
        {
            Items.Food["steak"], Items.Food["bacon"], Items.Food["chicken"],
            Items.Food["steak"], Items.Food["chicken"], Items.Food["chicken"],
            Items.Food["bacon"], Items.Food["chicken"], Items.Food["bacon"]
        };

        private static ItemModel[] victorias = new ItemModel[]
        {
            Items.Potions["damage_potion"], Items.Potions["protection_potion"],  Items.Potions["health_potion"],
            Items.Potions["damage_potion"], Items.Potions["protection_potion"],  Items.Potions["health_potion"],
            Items.Potions["damage_potion"], Items.Potions["protection_potion"],  Items.Potions["health_potion"],
        };

        private static ItemModel[] wilsons = new ItemModel[]
        {
            Items.Weapons["generic_club"], Items.Weapons["veteran_sword"], Items.Weapons["generic_spear"],
            Items.Weapons["generic_sword"], Items.Weapons["generic_club"], Items.Weapons["two_handed_sword"],
            Items.Weapons["generic_sword"], Items.Weapons["generic_club"], Items.Weapons["regular_sword"]
        };

        private static ItemModel[] stephens = new ItemModel[]
        {
            Items.Armors["horned_helmet"], Items.Armors["guardian_helmet"], Items.Armors["leather_hat"],
            Items.Armors["cross_helmet"], Items.Armors["regular_helmet"], Items.Armors["generic_helmet"],
            Items.Armors["leather_hat"], Items.Armors["cross_helmet"]
        };

        #endregion

        public static Dictionary<string, MapIcon> GlobalMap = new()
        {
            //{"showroom", new MapIcon("Шоурум", Location.ShowRoom, Items.PathTo("red_apple"), new System.Windows.Point(50, 50))},
            {"hearts", new MapIcon($"{app.FindResource("btnHearts")}", Location.Hearts, Items.PathTo("hearts"), new Point(10, 10))},
            {"spades", new MapIcon($"{app.FindResource("btnSpades")}", Location.Spades, Items.PathTo("spades"), new Point(60, 10))},
            {"diamonds", new MapIcon($"{app.FindResource("btnDiamonds")}", Location.Diamonds, Items.PathTo("diamonds"), new Point(110, 10))},
            {"clubs", new MapIcon($"{app.FindResource("btnClubs")}", Location.Clubs, Items.PathTo("clubs"), new Point(160, 10))},
        };


        public static Dictionary<string, TraderModel> AllTradersDefaultState = new()
        {
            {"food", new TraderModel(TraderType.Food, $"{app.FindResource("nFood")}", Items.Food.Values.ToArray(), Items.PathTo("green_houses"), new Point(10, 10), Location.ShowRoom)},
            {"weapons", new TraderModel(TraderType.Weapon, $"{app.FindResource("nWeapon")}", Items.Weapons.Values.ToArray(), Items.PathTo("green_houses"), new Point(60, 10), Location.ShowRoom)},
            {"armor", new TraderModel(TraderType.Armor, $"{app.FindResource("nArmor")}", Items.Armors.Values.ToArray(), Items.PathTo("green_houses"), new Point(110, 10), Location.ShowRoom)},
            {"artefacts", new TraderModel(TraderType.Artefact, $"{app.FindResource("nArtefact")}", Items.Artefacts.Values.ToArray(), Items.PathTo("green_houses"), new Point(160, 10), Location.ShowRoom)},
            {"potions", new TraderModel(TraderType.Universal, $"{app.FindResource("nPotion")}", Items.Potions.Values.ToArray(), Items.PathTo("green_houses"), new Point(210, 10), Location.ShowRoom)},

            // Червы
            {"peter", new TraderModel(TraderType.Food, $"{app.FindResource("nPeter")}", peters, Items.PathTo("green_houses"), new Point(70, 20), Location.Hearts)},
            {"patrick", new TraderModel(TraderType.Food, $"{app.FindResource("nPatrick")}", patricks, Items.PathTo("three_houses"), new Point(340, 90), Location.Hearts)},
            {"beniamin", new TraderModel(TraderType.Armor, $"{app.FindResource("nBeniamin")}", beniamins, Items.PathTo("chimney_house"), new Point(220, 110), Location.Hearts)},
            {"johans", new TraderModel(TraderType.Weapon, $"{app.FindResource("nJohans")}", johans, Items.PathTo("tiny_house"), new Point(90, 100), Location.Hearts)},

            // Пики
            {"beatrice", new TraderModel(TraderType.Food, $"{app.FindResource("nBeatrice")}", beatrices, Items.PathTo("pink_houses"), new Point(300, 215), Location.Spades)},
            {"alexander", new TraderModel(TraderType.Food, $"{app.FindResource("nAlexander")}", alexanders, Items.PathTo("big_old_house"), new Point(200, 25), Location.Spades)},
            {"frank", new TraderModel(TraderType.Weapon, $"{app.FindResource("nFrank")}", franks, Items.PathTo("long_house_left"), new Point(50, 100), Location.Spades)},
            {"edward", new TraderModel(TraderType.Armor, $"{app.FindResource("nEdward")}", edwards, Items.PathTo("long_house_right"), new Point(140, 200), Location.Spades)},

            // Бубны
            {"joshua", new TraderModel(TraderType.Food, $"{app.FindResource("nJoshua")}", joshuas, Items.PathTo("yellow_houses"), new Point(300, 60), Location.Diamonds)},
            {"benedict", new TraderModel(TraderType.Artefact, $"{app.FindResource("nBenedict")}", benedicts, Items.PathTo("big_old_house"), new Point(270, 120), Location.Diamonds)},
            {"daniel", new TraderModel(TraderType.Food, $"{app.FindResource("nDaniel")}", daniels, Items.PathTo("long_house_left"), new Point(330, 160), Location.Diamonds)},
            {"walters", new TraderModel(TraderType.Weapon, $"{app.FindResource("nWalters")}", walters, Items.PathTo("chimney_house"), new Point(265, 180), Location.Diamonds)},
            {"eugene", new TraderModel(TraderType.Armor, $"{app.FindResource("nEugene")}", eugenes, Items.PathTo("long_house_right"), new Point(340, 215), Location.Diamonds)},

            // Трефы
            {"luka", new TraderModel(TraderType.Food, $"{app.FindResource("nLuka")}", lukas, Items.PathTo("tiny_house"), new Point(180, 75), Location.Clubs)},
            {"arthur", new TraderModel(TraderType.Food, $"{app.FindResource("nArthur")}", arthurs, Items.PathTo("chimney_house"), new Point(75, 200), Location.Clubs)},
            {"witch", new TraderModel(TraderType.Universal, $"{app.FindResource("nWitch")}", victorias, Items.PathTo("cyan_tents"), new Point(230, 230), Location.Clubs)},
            {"wilson", new TraderModel(TraderType.Weapon, $"{app.FindResource("nWilson")}", wilsons, Items.PathTo("three_houses"), new Point(300, 10), Location.Clubs)},
            {"stephen", new TraderModel(TraderType.Armor, $"{app.FindResource("nStephen")}", stephens, Items.PathTo("long_house_left"), new Point(20, 230), Location.Clubs)},
        };

        public static Dictionary<string, Fighter> AllFightersDefaultState = new()
        {
            {"example", new Fighter("example", new List<ItemModel?>(3), null, Items.PathTo("red_tower"), new Point(10, 60), Location.ShowRoom)},

            // Червы
            {"hearts_robber_1", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>(3) {Items.Weapons["generic_club"], Items.Armors["leather_hat"],  null },
                new List<ItemModel?>(3){Items.Weapons["generic_club"] },
                Items.PathTo("ruined_tower"), new Point(270, 40), Location.Hearts, 100) },

            {"hearts_robber_2", new Fighter($"{app.FindResource("nRobber")}", new List<ItemModel?>(3) {Items.Weapons["generic_sword"], null,  null},
                new List<ItemModel?>(3){Items.Weapons["generic_sword"] },
                Items.PathTo("ruined_tower"), new Point(75, 180), Location.Hearts, 100)  },

            {"hearts_robber_3", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>(3) {Items.Weapons["generic_spear"], null,  null},
                new List<ItemModel?>(3){Items.Weapons["generic_spear"] },
                Items.PathTo("ruined_tower"), new Point(290, 180), Location.Hearts, 100) },

            {"hearts_knight_1", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>(3) {Items.Weapons["two_handed_sword"], Items.Armors["generic_helmet"], null},
                new List<ItemModel?>(2) {Items.Armors["generic_helmet"], Items.Potions["health_potion"]},
                Items.PathTo("green_tower"),
                new Point(165, 210),
                Location.Hearts)},

            {"hearts_knight_2", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>(3) {Items.Weapons["veteran_sword"], Items.Armors["cross_helmet"], null},
                new List<ItemModel?>(2) {Items.Armors["cross_helmet"], },
                Items.PathTo("green_tower"),
                new Point(230, 10),
                Location.Hearts)},

            {"hearts_knight_3", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>(3) {Items.Weapons["regular_sword"], Items.Armors["regular_helmet"], Items.Artefacts["ruby_ring"]},
                new List<ItemModel?>(1) {Items.Artefacts["ruby_ring"]},
                Items.PathTo("green_tower"),
                new Point(140, 45),
                Location.Hearts)},

            {"hearts_ace", new Fighter($"{app.FindResource("nHeartsAce")}",
                new List<ItemModel?>(3) {Items.Weapons["hearts_sword"], Items.Armors["elite_helmet"], Items.Artefacts["sapphire_necklace"] },
                new List<ItemModel?>() {Items.Weapons["hearts_sword"], Items.Armors["elite_helmet"], Items.Artefacts["sapphire_necklace"], Items.Potions["protection_potion"], Items.Potions["protection_potion"] },
                Items.PathTo("red_gates"), new Point(150, 150), Location.Hearts, 1000)
            },

            // Пики 
            {"spades_robber_1", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["veteran_sword"], null, Items.Artefacts["sapphire_ring"]},
                new List<ItemModel?>() {Items.Artefacts["sapphire_ring"]},
                Items.PathTo("ruined_tower"), new Point(100, 200), Location.Spades, 150) },

            {"spades_robber_2", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["generic_spear"], Items.Armors["generic_helmet"]},
                new List<ItemModel?>() {Items.Armors["generic_helmet"]},
                Items.PathTo("ruined_tower"), new Point(230, 40), Location.Spades, 200) },

            {"spades_robber_3", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["generic_spear"], Items.Armors["leather_hat"]},
                new List<ItemModel?>() {Items.Weapons["generic_spear"]},
                Items.PathTo("ruined_tower"), new Point(40, 230), Location.Spades, 125) },

            {"spades_knight_1", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["generic_spear"], Items.Armors["cross_helmet"]},
                new List<ItemModel?>() {Items.Armors["cross_helmet"]},
                Items.PathTo("blue_tower"), new Point(120, 120), Location.Spades) },

            {"spades_knight_2", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["two_handed_sword"], Items.Armors["horned_helmet"]},
                new List<ItemModel?>() {Items.Weapons["two_handed_sword"], Items.Potions["health_potion"] },
                Items.PathTo("blue_tower"), new Point(175, 65), Location.Spades) },

            {"spades_knight_3", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["generic_spear"], Items.Armors["guardian_helmet"], Items.Artefacts["ruby_ring"]},
                new List<ItemModel?>() {Items.Armors["guardian_helmet"]},
                Items.PathTo("blue_tower"), new Point(190, 130), Location.Spades) },

            {"spades_ace", new Fighter($"{app.FindResource("nSpadesAce")}",
                new List<ItemModel?>() {Items.Weapons["spades_spade"], Items.Armors["butcher_helmet"], Items.Artefacts["ruby_necklace"]},
                new List<ItemModel?>() {Items.Weapons["spades_spade"], Items.Armors["butcher_helmet"], Items.Artefacts["ruby_necklace"], Items.Potions["damage_potion"], Items.Potions["damage_potion"]},
                Items.PathTo("blue_gates"), new Point(300, 100), Location.Spades, 1000) },


            // Бубны
            {"diamonds_robber_1", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["veteran_sword"], Items.Armors["generic_helmet"], Items.Artefacts["amethyst_ring"]},
                new List<ItemModel?>() {Items.Artefacts["amethyst_ring"]},
                Items.PathTo("ruined_tower"), new Point(50, 150), Location.Diamonds, 200) },

            {"diamonds_robber_2", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["generic_club"], Items.Armors["horned_helmet"]},
                new List<ItemModel?>() {Items.Armors["horned_helmet"] },
                Items.PathTo("ruined_tower"), new Point(210, 140), Location.Diamonds, 175) },

            {"diamonds_robber_3", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["two_handed_sword"], Items.Armors["leather_hat"]},
                new List<ItemModel?>() {Items.Potions["health_potion"]},
                Items.PathTo("ruined_tower"), new Point(150, 45), Location.Diamonds, 250) },

            {"diamonds_knight_1", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["two_handed_sword"], Items.Armors["guardian_helmet"], Items.Artefacts["emerald_ring"]},
                new List<ItemModel?>() {Items.Artefacts["emerald_ring"]},
                Items.PathTo("red_tower"), new Point(100, 150), Location.Diamonds) },

            {"diamonds_knight_2", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["generic_spear"], Items.Armors["butcher_helmet"]},
                new List<ItemModel?>() {Items.Armors["butcher_helmet"], Items.Potions["damage_potion"] },
                Items.PathTo("red_tower"), new Point(40, 40), Location.Diamonds) },

            {"diamonds_knight_3", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["veteran_sword"], Items.Armors["elite_helmet"], Items.Artefacts["ruby_ring"]},
                new List<ItemModel?>() {Items.Armors["elite_helmet"]},
                Items.PathTo("red_tower"), new Point(120, 95), Location.Diamonds) },

            {"diamonds_ace", new Fighter($"{app.FindResource("nDiamondsAce")}",
                new List<ItemModel?>() {Items.Weapons["fencing_sword"], Items.Armors["diamonds_helmet"], Items.Artefacts["amethyst_necklace"]},
                new List<ItemModel?>() {Items.Weapons["fencing_sword"], Items.Armors["diamonds_helmet"], Items.Artefacts["amethyst_necklace"], Items.Potions["health_potion"], Items.Potions["damage_potion"], Items.Potions["protection_potion"] },
                Items.PathTo("red_gates"), new Point(145, 230), Location.Diamonds)},

            // Трефы 
            {"clubs_robber_1", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["generic_club"], null, Items.Artefacts["ruby_ring"] },
                new List<ItemModel?>() {Items.Artefacts["ruby_ring"] },
                Items.PathTo("ruined_tower"), new Point(150, 150), Location.Clubs, 200) },

            {"clubs_robber_2", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["veteran_sword"], Items.Armors["generic_helmet"]},
                new List<ItemModel?>() {Items.Armors["generic_helmet"] },
                Items.PathTo("ruined_tower"), new Point(40, 20), Location.Clubs, 150) },

            {"clubs_robber_3", new Fighter($"{app.FindResource("nRobber")}",
                new List<ItemModel?>() {Items.Weapons["regular_sword"], Items.Armors["leather_hat"]},
                new List<ItemModel?>() {Items.Weapons["regular_sword"]},
                Items.PathTo("ruined_tower"), new Point(340, 45), Location.Clubs, 125) },

            {"clubs_knight_1", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["generic_club"], Items.Armors["elite_helmet"], Items.Artefacts["sapphire_ring"]},
                new List<ItemModel?>() {Items.Potions["protection_potion"]},
                Items.PathTo("blue_tower"), new Point(200, 140), Location.Clubs) },

            {"clubs_knight_2", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["generic_spear"], Items.Armors["guardian_helmet"]},
                new List<ItemModel?>() {Items.Armors["guardian_helmet"]},
                Items.PathTo("blue_tower"), new Point(60, 80), Location.Clubs) },

            {"clubs_knight_3", new Fighter($"{app.FindResource("nKnight")}",
                new List<ItemModel?>() {Items.Weapons["two_handed_sword"], Items.Armors["cross_helmet"]},
                new List<ItemModel?>() {Items.Armors["cross_helmet"]},
                Items.PathTo("blue_tower"), new Point(100, 20), Location.Clubs) },

            {"clubs_ace", new Fighter($"{app.FindResource("nClubsAce")}",
                new List<ItemModel?>() {Items.Weapons["clubs_club"], Items.Armors["butcher_helmet"], Items.Artefacts["emerald_necklace"]},
                new List<ItemModel?>() { Items.Weapons["clubs_club"], Items.Armors["butcher_helmet"], Items.Artefacts["emerald_necklace"], Items.Potions["health_potion"], Items.Potions["health_potion"], Items.Potions["health_potion"] },
                Items.PathTo("blue_gates"), new Point(330, 150), Location.Clubs)},


        };

        public static void Initialize()
        {

        }
    }
}
