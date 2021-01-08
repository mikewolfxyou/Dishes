using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DishesApi.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DishAvailableDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
        AnyDay
    }
}