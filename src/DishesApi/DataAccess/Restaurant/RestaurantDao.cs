using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DishesApi.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace DishesApi.DataAccess.Restaurant
{
    public class RestaurantDao : IRestaurantDao
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger _logger;

        private const string Collection = "restaurant";
        private const int DefaultRestaurantFetchLimit = 10;

        public RestaurantDao(
            IDatabaseFactory databaseFactory,
            ILogger logger
        )
        {
            _databaseFactory = databaseFactory;
            _logger = logger;
        }

        public async Task<string> UpsertAsync(RestaurantDto restaurantDto)
        {
            if (string.IsNullOrEmpty(restaurantDto.RestaurantId))
            {
                restaurantDto.RestaurantId = ObjectId.GenerateNewId().ToString();
            }

            var filter = new BsonDocument("_id", restaurantDto.RestaurantId);

            var update = new BsonDocument("$set", new BsonDocument
            {
                {"Name", restaurantDto.Name},
                {"ShortDescription", restaurantDto.ShortDescription ?? ""},
                {"Country", restaurantDto.Country},
                {"City", restaurantDto.City},
                {"District", restaurantDto.District ?? ""},
                {"PostCode", restaurantDto.PostCode ?? ""}
            });

            try
            {
                var upsertResult =  await GetCollection().UpdateOneAsync(
                    filter, update, new UpdateOptions {IsUpsert = true}
                );

                if (upsertResult.IsAcknowledged && upsertResult.IsModifiedCountAvailable)
                {
                    return restaurantDto.RestaurantId;
                }
                
                _logger.Error("Upsert restaurant error");

                return string.Empty;
            }
            catch (Exception e)
            {
                _logger.Error("Exception in RestaurantDao Upsert: " + e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RestaurantDto>> GetAsync()
        {
            var result = new List<RestaurantDto>();
            try
            {
                result = await GetCollection().Find(new BsonDocument())
                    .Limit(DefaultRestaurantFetchLimit).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error("Exception in RestaurantDao Get: " + e.Message);
            }

            return result;
        }

        public async Task<RestaurantDto> GetAsync(string restaurantId)
        {
            try
            {
                var filter = Builders<RestaurantDto>.Filter.Eq("_id", restaurantId);
                using var result = await GetCollection().Find(filter).ToCursorAsync();
                while (await result.MoveNextAsync())
                {
                    return result.Current.First();
                }
            }
            catch (Exception e)
            {
                _logger.Error("Exception in RestaurantDao Get: " + e.Message);
            }

            return new RestaurantDto();
        }

        public async Task<RestaurantDto> DeleteAsync(string restaurantId)
        {
            try
            {
                var filter = new BsonDocument("_id", restaurantId);
                return await GetCollection().FindOneAndDeleteAsync(filter);
            }
            catch (Exception e)
            {
                _logger.Error("Exception in RestaurantDao Delete: " + e.Message);
            }

            return new RestaurantDto();
        }

        private IMongoCollection<RestaurantDto> GetCollection()
        {
            var database = _databaseFactory.GetDatabase();
            var collection = database.GetCollection<RestaurantDto>(Collection);
            try
            {
                return collection;
            }
            catch (Exception e)
            {
                _logger.Error("Exception in RestaurantDao GetCollection: " + e.Message);
                throw;
            }
        }
    }
}