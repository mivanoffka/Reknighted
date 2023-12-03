using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Collections
{
    public class Entities
    {
        public static Dictionary<string, IMappable> ShowRoomEntities = new Dictionary<string, IMappable>()
        {
            {"food", new TraderModel(TraderType.Food, "Еда", Items.Food.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(10, 10))},
            {"weapons", new TraderModel(TraderType.Weapon, "Оружие", Items.Weapons.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(60, 10))},
            {"armor", new TraderModel(TraderType.Armor, "Броня", Items.Armors.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(110, 10))},
            {"artefacts", new TraderModel(TraderType.Artefact, "Артефакты", Items.Artefacts.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(160, 10))},
            {"potions", new TraderModel(TraderType.Universal, "Зелья", Items.Potions.Values.ToArray(), Items.PathTo("green_houses"), new System.Windows.Point(210, 10))}
        };

        private static ItemModel[] peters = new ItemModel[]
        {
            Items.Food["apple_green"], Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["cherry"], Items.Food["strawberry"], Items.Food["lemon"],
            Items.Food["apple_green"], Items.Food["cherry"], Items.Food["apple_green"], Items.Food["apple_red"], Items.Food["peach"], Items.Food["apple_green"],
            Items.Food["apple_green"], Items.Food["peach"], Items.Food["strawberry"], Items.Food["cherry"], Items.Food["apple_red"], Items.Food["apple_green"], Items.Food["apple_green"],
            Items.Food["lemon"], Items.Food["strawberry"]
        };

        public static Dictionary<string, IMappable> HeartsEntities = new Dictionary<string, IMappable>()
        {
            {"peter", new TraderModel(TraderType.Food, "Садовод Пётр", peters, Items.PathTo("green_houses"), new System.Windows.Point(60, 10))}
        };

        public static Dictionary<string, Dictionary<string, IMappable>> AllEntities = new()
        {
            {"showroom", ShowRoomEntities}
        };

        public static void Initialize()
        {
 
        }
    }
}
