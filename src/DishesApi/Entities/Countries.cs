using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DishesApi.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Countries
    {
        Germany,
        Austria,
        Swiss
    }
}