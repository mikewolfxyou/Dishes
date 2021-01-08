using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DishesApi.DataAccess.Dish;
using DishesApi.Services.Validators;
using Serilog;

namespace DishesApi.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly IDishDao _dishDao;
        private readonly DishValidator _dishValidator;
        private readonly ILogger _logger;

        public DishRepository(IDishDao dishDao, ILogger logger, DishValidator dishValidator)
        {
            _dishDao = dishDao;
            _logger = logger;
            _dishValidator = dishValidator;
        }

        public async Task<bool> UpsertAsync(DishDto dishDto)
        {
            var dishValidation = _dishValidator.Validate(dishDto);

            if (!dishValidation.IsValid)
            {
                throw new ArgumentException(dishValidation.Erros.First().Message);
            }
            try
            {
                var upsertResult = await _dishDao.UpsertAsync(dishDto);

                if (upsertResult.IsAcknowledged && upsertResult.IsModifiedCountAvailable)
                {
                    return true;
                }

                _logger.Warning("Upsert dish failed");

                return false;
            }
            catch (Exception e)
            {
                _logger.Error("Exception when save dish: " + e.Message);

                return false;
            }
        }

        public async Task<IEnumerable<DishDto>> GetAsync(IEnumerable<string> dishIds)
        {
            try
            {
                return await _dishDao.GetAsync(dishIds);
                
            }
            catch (Exception e)
            {
                _logger.Error("Exception when get dish: " + e.Message);

                return new List<DishDto>();
            }
        }

        public async Task<IEnumerable<DishDto>> GetAsync(string restaurantId)
        {
            try
            {
                return await _dishDao.GetAsync(restaurantId);
            }
            catch (Exception e)
            {
                _logger.Error("Exception when get dish by restaurant id: " + e);
            }

            return new List<DishDto>();
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> dishIds)
        {
            try
            {
                var deleteResult = await _dishDao.DeleteAsync(dishIds);
                if (deleteResult.IsAcknowledged && deleteResult.DeletedCount == dishIds.Count())
                {
                    _logger.Warning("Delete count is not equal dish ids count");
                } 

                return true;
            } 
            catch (Exception e)
            {
                _logger.Error("Exception when delete dishes: " + e);

                return false;
            }
        }
    }
}