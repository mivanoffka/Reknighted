using Reknighted.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reknighted.Model;
using System.Security.Policy;

namespace Reknighted.Collections
{
    public static class Locations
    {
        public static Dictionary<string, List<MapIcon>> Locs = new()
        {
            {"showroom", MappablesToMapIcons(Entities.ShowRoomEntities.Values.ToList())},
            {"hearts", MappablesToMapIcons(Entities.HeartsEntities.Values.ToList())}
        };

        public static Dictionary<string, MapIcon> GlobalMap = new()
        {
            {"showroom", new MapIcon("Шоурум", Locs["showroom"], Items.PathTo("red_apple"), new Point(50, 50))},
            {"hearts", new MapIcon("Червы", Locs["hearts"], Items.PathTo("hearts"), new Point(10, 10))}
        };

        public static void Initialize()
        {

        }

        private static List<MapIcon> MappablesToMapIcons(List<IMappable> mappables)
        {
            List<MapIcon> mapIcons = new List<MapIcon>();
            foreach (var item in mappables)
            {
                mapIcons.Add(new MapIcon(item));
            }

            return mapIcons;
        }
    }
}
