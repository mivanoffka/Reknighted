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
        public static List<MapIcon> SpadesLocation = new List<MapIcon>();
        public static List<MapIcon> DiamondsLocation = new List<MapIcon>();
        public static List<MapIcon> TreflesLocation = new List<MapIcon>();
        public static List<MapIcon> HarlequinLocation = new List<MapIcon>();

        public static List<MapIcon> GlobalMap = new List<MapIcon>();

        public static Dictionary<string, List<MapIcon>> CityMaps = new Dictionary<string, List<MapIcon>> {{ "Червы", HeartsLocation}, { "Трефы", TreflesLocation }, { "Пики", SpadesLocation } , { "Бубны", DiamondsLocation }, { "Арлекин", HarlequinLocation } };


        public static void Initialize()
        {
            HeartsLocation.Add(new MapIcon(Traders.Peter));
            HeartsLocation.Add(new MapIcon(Traders.Victor));
            HeartsLocation.Add(new MapIcon(Traders.Leonid));
            HeartsLocation.Add(new MapIcon(Fighters.Simon));
            HeartsLocation.Add(new MapIcon(Traders.Johann));
            HeartsLocation.Add(new MapIcon(Fighters.HeartsGuardian));
            HeartsLocation.Add(new MapIcon(Traders.Jeweler));
            HeartsLocation.Add(new MapIcon(Traders.Potions));

        }

    }
}
