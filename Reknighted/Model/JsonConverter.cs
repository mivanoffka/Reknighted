using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reknighted.Model
{
    public class ItemModelConverter : JsonConverter<ItemModel>
    {
        public override ItemModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var type = root.GetProperty("$type").GetString();

                // Определите тип в соответствии с $type и создайте объект
                switch (type)
                {
                    case "food":
                        return JsonSerializer.Deserialize<FoodModel>(root.GetRawText(), options);
                    // добавьте другие ваши подтипы здесь

                    default:
                        throw new NotSupportedException($"Unsupported type: {type}");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, ItemModel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
