using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Services.Validators;
using Serilog;

namespace DishesApi.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IRestaurantDao _restaurantDao;
        private readonly ILogger _logger;
        private readonly RestaurantValidator _restaurantValidator;

        public RestaurantRepository(IRestaurantDao restaurantDao, ILogger logger, RestaurantValidator restaurantValidator)
        {
            _restaurantDao = restaurantDao;
            _logger = logger;
            _restaurantValidator = restaurantValidator;
        }

        //TODO Unit test
        public async Task<string> UpsertAsync(RestaurantDto restaurantDto)
        {
            var restaurantValidation = _restaurantValidator.Validate(restaurantDto);

            if (!restaurantValidation.IsValid)
            {
                throw new ArgumentException(restaurantValidation.Erros.First().Message);
            }
            
            try
            {
                return await _restaurantDao.UpsertAsync(restaurantDto);
            }
            catch (Exception e)
            {
                _logger.Error("Exception when save restaurant: " + e);

                return string.Empty;
            }
        }

        public async Task<IEnumerable<RestaurantDto>> GetAsync()
        {
            try
            {
                return await _restaurantDao.GetAsync();
            }
            catch (Exception e)
            {
                _logger.Error("Exception when get restaurant: " + e);
            }

            return new List<RestaurantDto>();
        }

        public async Task<RestaurantDto> GetAsync(string restaurantId)
        {
            try
            {
                return await _restaurantDao.GetAsync(restaurantId);
            }
            catch (Exception e)
            {
                _logger.Error("Exception when get restaurant by id: " + e);
            }

            return new RestaurantDto();
        }

        //TODO Unit test
        public async Task<bool> DeleteAsync(string restaurantId)
        {
            try
            {
                var deletedDto = await _restaurantDao.DeleteAsync(restaurantId);
                
                return deletedDto != null && deletedDto.RestaurantId.Equals(restaurantId);
            }
            catch (Exception e)
            {
                _logger.Error("Exception when delete restaurant: " + e);
            }

            return false;
        }
    }
}