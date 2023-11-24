using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reknighted.Model;

namespace Reknighted.Collections
{
    class Items
    {
        public static FoodModel Apple = new FoodModel("Яблоко", "Падает от яблони не сильно далеко.", 25, 5, PathTo("apple"));
        public static FoodModel Cheese = new FoodModel("Сыр", "От него пахнет плесенью. Но далеко не факт, что это дор-блю.", 50, 20, PathTo("cheese"));

        public static WeaponModel Sword = new WeaponModel("Меч", "Им можно кого-то убить. Наверное...", 300, 15, Images.Sources.Sword);

        public static ArmorModel Helmet = new ArmorModel("Шлем", "Защищает голову, но не разум", 250, 10, Images.Sources.Helmet);
        public static ArmorModel HornsHelmet = new ArmorModel("Шлем с рожками", "Возможно, от этих рожек есть какая-то польза кроме утяжеления доспеха. Но это не точно.", 450, 25, Images.Sources.HornsHelmet);

        public static ArtefactModel BlueRing = new ArtefactModel("Кольцо с сапфиром", "Носить кольцо под перчатками доспехов звучит как не самая рациональная идея.", 0, Buff.Protection, 1.5, PathTo("blue_ring"));

        public static void Initialize()
        {
        }

        private static string PathTo(string name)
        {
            return "Images\\" + name + ".png";
        } 
    }
}
