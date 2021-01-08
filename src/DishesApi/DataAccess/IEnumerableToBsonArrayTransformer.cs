using System.Collections.Generic;
using MongoDB.Bson;

namespace DishesApi.DataAccess
{
    public interface IEnumerableToBsonArrayTransformer<in T>
    {
        BsonArray Create(IEnumerable<T> items);
    }
}