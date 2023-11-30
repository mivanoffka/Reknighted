using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Collections
{
    public class Traders
    {
        public static TraderModel Peter;
        public static TraderModel Victor;
        public static TraderModel Alexander;
        public static TraderModel Leonid;
        public static TraderModel Johann;

        public static void Initialize()
        {
            ItemModel?[] items = new ItemModel?[]
            {
                Items.AppleGreen.Copy(), Items.AppleRed.Copy(), Items.AppleGreen.Copy(), Items.AppleGreen.Copy(), Items.AppleRed.Copy(), Items.AppleGreen.Copy(),
                Items.AppleGreen.Copy(), Items.AppleGreen.Copy(), Items.AppleGreen.Copy(), Items.AppleRed.Copy(), Items.AppleGreen.Copy(), Items.AppleGreen.Copy(),
                Items.AppleRed.Copy(), Items.AppleGreen.Copy(), Items.AppleGreen.Copy(), Items.AppleGreen.Copy(), Items.AppleGreen.Copy(), Items.AppleRed.Copy(),

            };
            Peter = new TraderModel(TraderType.Food, "Садовод Пётр", items, Items.PathTo("green_houses"), new System.Windows.Point(60, 75));

            items = new ItemModel?[]
            {
                Items.Chicken, Items.Bacon, Items.BoarHead, Items.Steak, Items.Chicken, Items.Chicken, Items.Steak, Items.Chicken, Items.Steak, Items.Bacon, Items.Bacon, Items.Bacon, Items.Chicken,
            };
            Victor = new TraderModel(TraderType.Food, "Мясник Виктор", items, Items.PathTo("yellow_houses"), new System.Windows.Point(145, 180));

            items = new ItemModel?[] { Items.Weapons["generic_sword"].Copy(), Items.Weapons["generic_spear"].Copy(), Items.Weapons["generic_club"].Copy(),
                                        Items.Weapons["generic_spear"].Copy(), Items.Weapons["generic_sword"].Copy(), Items.Weapons["hearts_sword"].Copy(),
                                        Items.Weapons["generic_club"].Copy(), Items.Weapons["generic_sword"].Copy(), Items.Weapons["generic_spear"].Copy(),
                                        Items.Weapons["generic_spear"].Copy(), Items.Weapons["generic_sword"].Copy(), Items.Weapons["generic_club"].Copy(),
                                        Items.Weapons["fencing_sword"].Copy(), Items.Weapons["hearts_sword"].Copy(), Items.Weapons["generic_sword"].Copy(),
                                        Items.Weapons["generic_sword"].Copy(), Items.Weapons["generic_spear"].Copy(), Items.Weapons["generic_sword"].Copy(),

            };
            //items = Items.Weapons.Values.ToArray();
            {
                //Items.Sword, Items.FencingSword, Items.DiamondsSword, Items.HeartsSword, Items.HeavySword, Items.GenericClub, Items.GenericSpear, Items.Spade
            };
            Leonid = new TraderModel(TraderType.Weapon, "Оружейник Леонид", items, Items.PathTo("chimney_house"), new System.Windows.Point(200, 123));

            items = new ItemModel?[] { Items.Armors["guardian_helmet"].Copy() };
            Johann = new TraderModel(TraderType.Armor, "Бронник Иоанн", items, Items.PathTo("tiny_house"), new System.Windows.Point(150, 250));
        }
    }
}
