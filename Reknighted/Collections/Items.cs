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
        public static Dictionary<string, FoodModel> Food = new()
        {
            {"apple_green", new FoodModel("Зелёное яблоко", "Падает от яблони не сильно далеко.", 15, 10, PathTo("green_apple"))},
            {"apple_red",  new FoodModel("Красное яблоко", "Возможно, оно чего-то стыдится.", 20, 16, PathTo("red_apple"))},
            {"cherry", new FoodModel("Вишенка", "Жаль, что не на торте", 5, 4, PathTo("cherry"))},
            {"strawberry", new FoodModel("Клубничка", "-", 15, 10, PathTo("strawberry"))},
            {"peach", new FoodModel("Персик", "-", 20, 16, PathTo("peach"))},
            {"lemon", new FoodModel("Лимон", "Лакомство для извращенца.", 30, 24, PathTo("lemon"))},

            {"tomato", new FoodModel("Помидор", "Красный, как... он сам?", 25, 20, PathTo("tomato"))},
            {"pickle", new FoodModel("Огурец", "Он колючий. Приказано бояться.", 20, 16, PathTo("pickle"))},
            {"potato", new FoodModel("Картошка", "Ну зачем есть с ней мясо? Ну сьешь ты с рыбой, с селёдкой.", 30, 24, PathTo("potato"))},
            {"pepper_green", new FoodModel("Зелёный перец", "Он созревший, просто так сейчас модно.", 15, 10, PathTo("pepper_green"))},
            {"pepper_red",  new FoodModel("Красный перец", "Как зелёный, только круче.", 20, 16, PathTo("pepper_red"))},
            {"onion", new FoodModel("Лук", "Я не плачу, это просто слёзы...", 5, 4, PathTo("onion"))},

            {"honey", new FoodModel("Мёд", "Труд всей жизни целого улья", 150, 160, PathTo("honey"))},
            {"jam", new FoodModel("Варенье", "Не о такой судьбе мечтали ягодки", 50, 60, PathTo("jam"))},
            {"waffles", new FoodModel("Вафли", "-", 30, 24, PathTo("waffles"))},
            {"pretzel", new FoodModel("Крендель", "Произведение искусства на тему \"заворот кишок\"", 20, 16, PathTo("pretzel"))},
            {"cookie", new FoodModel("Печенька", "Печёнка в костюме печеньки", 15, 10, PathTo("cookie"))},
            {"candy", new FoodModel("Конфетка", "Маленькая и незначительная", 5, 4, PathTo("candy"))},

            {"chicken",  new FoodModel("Курочка", "Добегалась.", 50, 60, PathTo("chicken"))},
            {"bacon",  new FoodModel("Бекон", "Совершенно точно некошерно.", 75, 90, PathTo("bacon"))},
            {"steak",  new FoodModel("Говяжий стейк", "Медиум-велл? Нет, блин, экстрасенс-бэд.", 100, 120, PathTo("steak"))},
            {"pepperoni", new FoodModel("Колбаса", "Рога, копыта и хвосты в аппетитной оболочке из кишок.", 150, 160, PathTo("pepperoni")) },
            {"eggs", new FoodModel("Яичница", "Глазастая.", 25, 20, PathTo("eggs"))},
            {"cheese", new FoodModel("Сыр", "От него пахнет плесенью. Но далеко не факт, что это дор-блю.", 40, 40, PathTo("cheese"))}
        };

        public static Dictionary<string, ArtefactModel> Artefacts = new()
        {
            {"sapphire_ring", new ArtefactModel("Кольцо с сапфиром", "-", 1000, 100, 1.5, Buff.Protection, PathTo("sapphire_ring")) },
            {"ruby_ring", new ArtefactModel("Кольцо с рубином", "-", 1000, 100, 1.5, Buff.Damage, PathTo("ruby_ring")) },
            {"emerald_ring", new ArtefactModel("Кольцо с изумрудом", "-", 1000, 100, 1.5, Buff.Health, PathTo("emerald_ring")) },
            {"amethyst_ring", new ArtefactModel("Кольцо с аметистом", "-", 1000, 100, 1.5, Buff.Wealth, PathTo("amethyst_ring")) },
            {"sapphire_necklace", new ArtefactModel("Ожерелье с сапфиром", "-", 2000, 100, 2, Buff.Protection, PathTo("sapphire_necklace")) },
            {"ruby_necklace", new ArtefactModel("Ожерелье с рубином", "-", 2000, 100, 2, Buff.Damage, PathTo("ruby_necklace")) },
            {"emerald_necklace", new ArtefactModel("Ожерелье с изумрудом", "-", 2000, 100, 2, Buff.Health, PathTo("emerald_necklace")) },
            {"amethyst_necklace", new ArtefactModel("Кольцо с аметистом", "-", 2000, 100, 2, Buff.Wealth, PathTo("amethyst_necklace")) },
        };

        public static Dictionary<string, WeaponModel> Weapons = new Dictionary<string, WeaponModel>()
        {   
            {"generic_sword", new WeaponModel("Меч", "Ничем не примечателен. Предположительно, может наносить режущие ранения.", 250, 150, 25, PathTo("generic_sword"))},
            {"generic_club", new WeaponModel("Булава", "Ничем непримечательна. Служит долго, но бьёт не сильно больно", 250, 250, 15, PathTo("generic_club"))},
            {"generic_spear", new WeaponModel("Копьё", "Ничем не примечательно. Вам очень повезёт, если оно не сломается после первого же удара.", 250, 100, 35, PathTo("generic_spear")) },

            {"two_handed_sword", new WeaponModel("Двуручный меч", "С таким не стыдно и в люди выйти.", 650, 300, 45, PathTo("two_handed_sword"))},
            {"veteran_sword",  new WeaponModel("Меч ветерана", "Основное его предназначение – красиво висеть на стеночке.", 300, 100, 15, PathTo("veteran_sword"))},
            {"regular_sword", new WeaponModel("Армейский меч", "С таким же ты прошёл всю войну. По крайней мере, по документам.", 400, 150, 35, PathTo("regular_sword"))},

            {"hearts_sword", new WeaponModel("Червонный клинок", "-.", 550, 150, 45, PathTo("hearts_sword"))},
            {"spades_spade", new WeaponModel("Пиковое копьё", "-", 550, 150, 45, PathTo("spade"))},
            {"clubs_club", new WeaponModel("Трефовая булава", "-", 550, 150, 45, PathTo("clubs_club"))},

            {"fencing_sword",  new WeaponModel("Шпага для фехтования", "Создана исключительно в спортивных целях. Спорт, однако, бывает смертельно опасным.", 650, 50, 75, PathTo("fencing_sword"))}
        };

        public static Dictionary<string, PotionModel> Potions = new Dictionary<string, PotionModel>()
        {
            {"health_potion", new PotionModel("Флакон с зелёной жидкостью", "Одному богу известно, что случится, если это выпить", 500, 10, Buff.Protection, PathTo("health_potion"))},
            {"protection_potion", new PotionModel("Флакон с голубой жидкостью", "Одному богу известно, что случится, если это выпить", 500, 10, Buff.Protection, PathTo("protection_potion"))},
            {"damage_potion", new PotionModel("Флакон с красной жидкостью", "Одному богу известно, что случится, если это выпить", 500, 10, Buff.Protection, PathTo("damage_potion"))   }
        };

        public static Dictionary<string, ArmorModel> Armors = new Dictionary<string, ArmorModel>()
        {
            {"leather_hat", new ArmorModel("Кожаная шапка", "Защищает разве что от дождя", 50, 200, 5, PathTo("leather_hat")) },
            {"generic_helmet", new ArmorModel("Шлем", "Ничем не примечательный шлем", 250, 250, 25, PathTo("generic_helmet")) },

            {"regular_helmet", new ArmorModel("Армейский шлем", "Судя по потерям в рядах солдат – защищает он не очень.", 400, 150, 35, PathTo("regular_helmet"))},
            {"cross_helmet", new ArmorModel("Командирский шлем", "Добавляет несколько баллов к уверенности. Но шансы выжить не повышает", 400, 150, 35, PathTo("cross_helmet"))},
            {"horned_helmet", new ArmorModel("Шлем с рожками", "Производитель заявляет: рожки не просто утяжеляют шлем, но и заметно улучшают его свойства. Призываем не верить производителю", 600, 300, 40, PathTo("horned_helmet"))}, 

            {"guardian_helmet", new ArmorModel("Шлем придворного гвардейца", "-", 800, 150, 50, PathTo("guardian_helmet")) },
            {"butcher_helmet", new ArmorModel("Шлем палача", "Скрывает лицо, чтобы оно не сильно пугало ваших противников. Если они, конечно, боятся слёз.", 750, 300, 40, PathTo("butcher_helmet"))},
            {"elite_helmet", new ArmorModel("Шлем королевской гвардии", "Престижнее некуда.", 1500, 300, 65, PathTo("elite_helmet"))},

            {"diamonds_helmet", new ArmorModel("Бубновый шлем", "-", 2000, 500, 80, PathTo("diamonds_helmet"))},
        };

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
