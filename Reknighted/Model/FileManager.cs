using Reknighted.Controller;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace Reknighted.Model
{
    public static class FileManager
    {
        public static void Save(int slot)
        {
            string path = $"Saves\\{slot}.json";
            if (!Directory.Exists("Saves"))
            {
                Directory.CreateDirectory("Saves");
            }
            var properties = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            ObjectsState states = new ObjectsState(Game.PlayerModel, Game.AllTraders, Game.AllFighters);
            var json = JsonSerializer.Serialize(states, properties);
            File.WriteAllText(path, json);
        }

        public static ObjectsState? LoadProgress(int slot) 
        {
            ObjectsState? states = null;
            string path = $"Saves\\{slot}.json";
            var properties = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            
            if(File.Exists(path))
            {
                states = JsonSerializer.Deserialize<ObjectsState>(File.ReadAllText(path), properties);
            }
            else
            {
                MessageBox.Show(Application.Current.FindResource("msgNoSlotInfo").ToString());
            }

            return states;
        }

        public static Dictionary<string, ItemModel> LoadAssets(string dictName)
        {
            CultureInfo currLang = App.Language;
            string path = $"lang\\{currLang}\\{dictName}.json";

            if (!File.Exists(path))
            {
                MessageBox.Show($"Файл по пути {path} не найден!");
                return null;
            }

            var file = File.ReadAllText(path);
            var properties = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            var dict = JsonSerializer.Deserialize<Dictionary<string, ItemModel>>(file, properties);

            return dict;
        }
    }
}
