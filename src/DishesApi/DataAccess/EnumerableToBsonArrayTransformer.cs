using System.Collections.Generic;
using MongoDB.Bson;

namespace DishesApi.DataAccess
{
    public class EnumerableToBsonArrayTransformer<T> : IEnumerableToBsonArrayTransformer<T> 
    {
        public BsonArray Create(IEnumerable<T> items)
        {
            var bArray = new BsonArray();

            foreach (var item in items)
            {
                bArray.Add(item.ToString());
            }

            return bArray;
        }
    }
}