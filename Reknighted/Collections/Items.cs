using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reknighted.Model;

namespace Reknighted.Collections
{
    class Items
    {
        public static FoodModel AppleGreen = new FoodModel("Зелёное яблоко", "Падает от яблони не сильно далеко.", 15, 5, PathTo("green_apple"));
        public static FoodModel AppleRed = new FoodModel("Красное яблоко", "Возможно, оно чего-то стыдится.", 20, 8, PathTo("red_apple"));

        #region Мясо

        public static FoodModel Chicken = new FoodModel("Курочка", "Добегалась.", 65, 35, PathTo("chicken"));
        public static FoodModel Bacon = new FoodModel("Бекон", "Совершенно точно некошерно.", 80, 40, PathTo("bacon"));
        public static FoodModel Steak = new FoodModel("Говяжий стейк", "Медиум-велл? Нет, блин, экстрасенс-бэд.", 100, 75, PathTo("steak"));

        #endregion

        public static FoodModel Cheese = new FoodModel("Сыр", "От него пахнет плесенью. Но далеко не факт, что это дор-блю.", 50, 20, PathTo("cheese"));

        public static Dictionary<string, ArtefactModel> Artefacts = new()
        {
            {"sapphire_ring", new ArtefactModel("Кольцо с сапфиром", "-", 1000, Buff.Protection, 1.5, PathTo("sapphire_ring")) },
            {"ruby_ring", new ArtefactModel("Кольцо с рубином", "-", 1000, Buff.Damage, 1.5, PathTo("ruby_ring")) },
            {"emerald_ring", new ArtefactModel("Кольцо с изумрудом", "-", 1000, Buff.Health, 1.5, PathTo("emerald_ring")) }
        };

        public static Dictionary<string, WeaponModel> Weapons = new Dictionary<string, WeaponModel>()
        {
            {"generic_sword", new WeaponModel("Ничем не примечательный меч", "-", 250, 150, 25, PathTo("generic_sword"))},
            {"generic_club", new WeaponModel("Ничем не примечательная булава", "-", 250, 250, 15, PathTo("generic_club"))},
            {"generic_spear", new WeaponModel("Ничем не примечательное копьё", "-", 250, 100, 35, PathTo("generic_spear")) },

            {"hearts_sword", new WeaponModel("Червонный клинок", "-.", 550, 150, 45, PathTo("hearts_sword"))},

            {"fencing_sword",  new WeaponModel("Шпага для фехтования", "Создана исключительно в спортивных целях. Спорт, однако, бывает смертельно опасным.", 650, 50, 75, PathTo("fencing_sword"))}
        };

        public static Dictionary<string, PotionModel> Potions = new Dictionary<string, PotionModel>()
        {
            {"health_potion", new PotionModel("Флакон с зелёной жидкостью", "Одному богу известно, что случится, если это выпить", 500, 10, PathTo("health_potion"), Buff.Health) },
            {"protection_potion", new PotionModel("Флакон с голубой жидкостью", "Одному богу известно, что случится, если это выпить", 500, 10, PathTo("protection_potion"), Buff.Protection)},
            {"damage_potion", new PotionModel("Флакон с красной жидкостью", "Одному богу известно, что случится, если это выпить", 500, 10, PathTo("damage_potion"), Buff.Damage)   }

        };

        public static WeaponModel Sword = new WeaponModel("Меч", "Им можно кого-то убить. Наверное...", 300, 100, 15, PathTo("common_sword"));
        //public static WeaponModel FencingSword = new WeaponModel("Меч для фехтования", "Создан исключительно в спортивных целях. Спорт, однако, бывает смертельно опасным.", 450, 100, 25, PathTo("fencing_sword"));
        //public static WeaponModel DiamondsSword = new WeaponModel("Бубновый клинок", "Разработан специально для Бубновой армии. Сочетает в себе низкую цену и долговечность.", 200, 300, 20, PathTo("diamonds_sword"));
        //public static WeaponModel HeavySword = new WeaponModel("Тяжелый меч", "-", 600, 200, 45, PathTo("heavy_sword"));
        //public static WeaponModel Spade = new WeaponModel("Пиковое копьё", "-", 750, 250, 60, PathTo("spade"));

        //public static WeaponModel RegularSword = new WeaponModel("Образцовый меч", "Такие же стоят на вооружении у регулярной армии.", 400, 150, 25, PathTo("regular_sword"));

        public static Dictionary<string, ArmorModel> Armors = new Dictionary<string, ArmorModel>()
        {
            {"generic_helmet", new ArmorModel("Шлем", "Ничем не примечательный шлем", 250, 150, 20, PathTo("generic_helmet")) },
            {"guardian_helmet", new ArmorModel("Шлем гвардейца", "Это носит вся наша армия. И ты тоже носил.", 250, 100, 100, PathTo("guardian_helmet")) }
    
        };

        public static ArmorModel Helmet = new ArmorModel("Шлем", "Защищает голову, но не разум", 250, 100, 10, Images.Sources.Helmet);
        public static ArmorModel HornsHelmet = new ArmorModel("Шлем с рожками", "Возможно, от этих рожек есть какая-то польза кроме утяжеления доспеха. Но это не точно.", 450, 200, 25, Images.Sources.HornsHelmet);
        public static ArmorModel GoldenHelmet = new ArmorModel("Шлем с позолотой", "Наградной шлем для отличившихся ветеранов войны. Как нетрудно догадаться, лучшее его применение – сдать в ломбард.", 450, 100, 15, PathTo("rare_helmet"));
        public static ArmorModel BoarHead = new ArmorModel("Свиная бошка", "Не факт, что противник оценит ваш косплей Иноскэ, но чем бы дитя не тешилось...", 100, 50, 10, PathTo("boar"));

        public static ArmorModel ButcherHelmet = new ArmorModel("Шлем палача", "Скрывает лицо, чтобы оно не сильно пугало ваших противников. Если они, конечно, боятся слёз.", 1000, 500, 75, PathTo("butcher_helmet"));



        public static void Initialize()
        {
        }

        public static string PathTo(string name)
        {   
            try
            {
                return Directory.GetFiles("Images", $"{name}.png", SearchOption.AllDirectories)[0];
            }
            catch
            {
                MessageBox.Show("Файл не найден. " + name);
                throw;
            }
        } 
    }
}
