using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace Reknighted.Model
{
    public static class FileManager
    {
        public static void Save()
        {
            string path = "Saves";

            //var properties = new JsonSerializerOptions()
            //{
            //    WriteIndented = true,
            //};
            //var json = JsonSerializer.Serialize<Dictionary<string, ArtefactModel>>(Items.Artefacts, properties);
            //File.WriteAllText($"{path}/Artefacts.json", json);

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
