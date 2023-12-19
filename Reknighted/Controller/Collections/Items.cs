using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reknighted.Controller.Serialization;
using Reknighted.Model.Items;

namespace Reknighted.Controller.Collections
{
    public class Items
    {
        public static Dictionary<string, FoodModel> Food;

        public static Dictionary<string, ArtefactModel> Artefacts;

        public static Dictionary<string, WeaponModel> Weapons = new Dictionary<string, WeaponModel>();

        public static Dictionary<string, PotionModel> Potions = new Dictionary<string, PotionModel>();

        public static Dictionary<string, ArmorModel> Armors = new Dictionary<string, ArmorModel>();

        public static void Initialize()
        {   
            try
            {
                Food = FileManager.LoadAssets("Food").ToDictionary(kv => kv.Key, kv => (FoodModel)kv.Value);
                Artefacts = FileManager.LoadAssets("Artefacts").ToDictionary(kv => kv.Key, kv => (ArtefactModel)kv.Value);
                Weapons = FileManager.LoadAssets("Weapons").ToDictionary(kv => kv.Key, kv => (WeaponModel)kv.Value);
                Potions = FileManager.LoadAssets("Potions").ToDictionary(kv => kv.Key, kv => (PotionModel)kv.Value);
                Armors = FileManager.LoadAssets("Armors").ToDictionary(kv => kv.Key, kv => (ArmorModel)kv.Value);

                try
                {
                    Game.Logger.Info("Assets successfuly loaded");
                }
                catch
                {

                }

            }
            catch (Exception ex) 
            {
                try
                {
                    Game.Logger.Error("Cannot load assets. " + ex.Message);
                }
                catch
                {

                }

            }
 
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
                Game.Logger.Error("Файл не найден. " + name);
                throw;
            }
        }
    }
}
