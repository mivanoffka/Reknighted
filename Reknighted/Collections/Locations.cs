using Reknighted.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reknighted.Model;

namespace Reknighted.Collections
{
    public static class Locations
    {
        public static List<MapIcon> HeartsLocation = new List<MapIcon>();
        public static List<MapIcon> ClubsLocation = new List<MapIcon>();
        public static List<MapIcon> DiamondsLocation = new List<MapIcon>();
        public static List<MapIcon> TreflesLocation = new List<MapIcon>();
        public static List<MapIcon> HarlequinLocation = new List<MapIcon>();

        public static List<MapIcon> GlobalMap = new List<MapIcon>();

        public static Dictionary<string, List<MapIcon>> CityMaps = new Dictionary<string, List<MapIcon>> {{ "Червы", HeartsLocation}, { "Трефы", TreflesLocation }, { "Пики", ClubsLocation } , { "Бубны", DiamondsLocation }, { "Арлекин", HarlequinLocation } };


        public static void Initialize()
        {
            MapIcon mapIcon = new MapIcon(Items.PathTo("apple"), Traders.Peter);
            mapIcon.Position = new Point(30, 40);
            mapIcon.Description = "Торговец Петя.";
            HeartsLocation.Add(mapIcon);

            MapIcon fighterIcon = new MapIcon(Items.PathTo("common_helmet"), Fighters.Simon);
            fighterIcon.Position = new Point(150, 150);
            fighterIcon.Description = "Саймон.";
            TreflesLocation.Add(fighterIcon);

            MapIcon hearts = new MapIcon(Items.PathTo("hearts"), City.Hearts);
            hearts.Name = "Червы";
            hearts.Position = new Point(10, 10);
            hearts.Description = "Столица земель Червей";
            GlobalMap.Add(hearts);

            MapIcon trefles = new MapIcon(Items.PathTo("trefles"), City.Hearts);
            trefles.Name = "Трефы";
            trefles.Position = new Point(60, 100);
            trefles.Description = "Столица земель Треф";
            GlobalMap.Add(trefles);
        }

    }
}
