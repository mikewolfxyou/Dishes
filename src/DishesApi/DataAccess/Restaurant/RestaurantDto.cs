using DishesApi.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DishesApi.DataAccess.Restaurant
{
    public class RestaurantDto : IDto
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RestaurantId { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public Countries? Country { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string PostCode { get; set; }
    }
}