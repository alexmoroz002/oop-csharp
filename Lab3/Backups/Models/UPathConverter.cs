using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zio;

namespace Backups.Models;

public class UPathConverter : JsonConverter<UPath>
{
    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, UPath value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override UPath ReadJson(JsonReader reader, Type objectType, UPath existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var holder = new UPath((string)jo["FullName"] ?? throw new NotImplementedException());
        return holder;
    }
}