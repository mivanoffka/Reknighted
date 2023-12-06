using Reknighted.Controller;
using System.Collections.Generic;
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
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var json = JsonSerializer.Serialize <PlayerModel>(Game.PlayerModel, properties);
            File.WriteAllText(path, json);

        }

        public static void LoadProgress(int slot) 
        {
            string path = $"Saves\\{slot}.json";
            var properties = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            
            var playerInfo = JsonSerializer.Deserialize<PlayerModel>(File.ReadAllText(path), properties);
            Game.PlayerModel = playerInfo;
            Game.PlayerView = new PlayerView();
            Game.PlayerView.Model = Game.PlayerModel;
        }

        public static Dictionary<string, ItemModel> LoadAssets(string dictName)
        {
            string path = $"lang\\ru-RU\\{dictName}.json";

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
