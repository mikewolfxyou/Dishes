using System.Collections.Generic;
using DishesApi.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DishesApi.DataAccess.Dish
{
    public class DishDto : IDto
    {
        [BsonId(IdGenerator=typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DishId { get; set; }
        
        public string RestaurantId { get; set; }
        
        public string Name { get; set; }
        
        public string ShortDescription { get; set; }
        
        public decimal Price { get; set; }
        
        public string Category { get; set; }
        
        public IEnumerable<DishAvailableMeal> AvailableMeal { get; set; }
        
        public IEnumerable<DishAvailableDay> AvailableDay { get; set; }
        
        public bool Active { get; set; }
        
        public int AwaitingTime { get; set; }
    }
}