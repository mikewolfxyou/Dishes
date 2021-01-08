using MongoDB.Driver;

namespace DishesApi.Infrastructure
{
    public interface IDatabaseFactory
    {
        IMongoDatabase GetDatabase();
    }
}