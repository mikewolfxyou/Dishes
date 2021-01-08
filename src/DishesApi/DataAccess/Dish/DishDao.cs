using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DishesApi.Entities;
using DishesApi.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace DishesApi.DataAccess.Dish
{
    public class DishDao : IDishDao
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger _logger;
        private readonly IEnumerableToBsonArrayTransformer<DishAvailableMeal> _dishAvailableMealToBsonArrayTransformer;
        private readonly IEnumerableToBsonArrayTransformer<DishAvailableDay> _dishAvailableDayToBsonArrayTransformer;

        private const string Collection = "dish";

        public DishDao(
            IDatabaseFactory databaseFactory,
            ILogger logger,
            IEnumerableToBsonArrayTransformer<DishAvailableMeal> dishAvailableMealToBsonArrayTransformer, 
            IEnumerableToBsonArrayTransformer<DishAvailableDay> dishAvailableDayToBsonArrayTransformer
        ) {
            _databaseFactory = databaseFactory;
            _logger = logger;
            _dishAvailableMealToBsonArrayTransformer = dishAvailableMealToBsonArrayTransformer;
            _dishAvailableDayToBsonArrayTransformer = dishAvailableDayToBsonArrayTransformer;
        }

        public async Task<UpdateResult> UpsertAsync(DishDto dishDto)
        {
            if (string.IsNullOrEmpty(dishDto.DishId))
            {
                dishDto.DishId = ObjectId.GenerateNewId().ToString();
            }

            var filter = new BsonDocument("_id", dishDto.DishId);
            var update = new BsonDocument("$set", new BsonDocument
            {
                {"Name", dishDto.Name},
                {"ShortDescription", dishDto.ShortDescription},
                {"RestaurantId", dishDto.RestaurantId},
                {"Price", dishDto.Price},
                {"Category", dishDto.Category},
                {"AvailableMeal", _dishAvailableMealToBsonArrayTransformer.Create(dishDto.AvailableMeal)},
                {"AvailableDay", _dishAvailableDayToBsonArrayTransformer.Create(dishDto.AvailableDay)},
                {"Active", dishDto.Active},
                {"AwaitingTime", dishDto.AwaitingTime},
            });

            try
            {
                return await GetCollection().UpdateOneAsync(
                    filter, update, new UpdateOptions {IsUpsert = true}
                );
            }
            catch (Exception e)
            {
                _logger.Error("Exception in DishDao Upsert: " + e);
                throw;
            }
        }

        public async Task<IEnumerable<DishDto>> GetAsync(IEnumerable<string> dishIds)
        {
            var filter = Builders<DishDto>.Filter.AnyIn("_id", dishIds);
            try
            {
                return (await GetCollection().FindAsync(filter)).ToEnumerable();
            }
            catch (Exception e)
            {
                _logger.Error("Exception in DishDao Get: " + e);
                return new List<DishDto>();
            }
        }

        public async Task<IEnumerable<DishDto>> GetAsync(string restaurantId)
        {
            var result = new List<DishDto>();
            try
            {
                var filter = Builders<DishDto>.Filter.Eq("RestaurantId", restaurantId);
                using var findResult = await GetCollection().Find(filter).ToCursorAsync();
                while (await findResult.MoveNextAsync())
                {
                    result.AddRange(findResult.Current);
                }
            }
            catch (Exception e)
            {
                _logger.Error("Exception in RestaurantDao Get: " + e.Message);
            }

            return result;
        }

        public async Task<DeleteResult> DeleteAsync(IEnumerable<string> dishIds)
        {
            var filter = Builders<DishDto>.Filter.AnyIn("_id", dishIds);
            try
            {
                return await GetCollection().DeleteManyAsync(filter);
            }
            catch (Exception e)
            {
                _logger.Error("Exception in DishDao Delete: " + e);

                throw;
            }
        }

        private IMongoCollection<DishDto> GetCollection()
        {
            var database = _databaseFactory.GetDatabase();
            var collection = database.GetCollection<DishDto>(Collection);
            try
            {
                return collection;
            }
            catch (Exception e)
            {
                _logger.Error("Exception in DishDao GetCollection: " + e);
                throw;
            }
        }
    }
}