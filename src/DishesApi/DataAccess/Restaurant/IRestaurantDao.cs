using System.Collections.Generic;
using System.Threading.Tasks;

namespace DishesApi.DataAccess.Restaurant
{
    public interface IRestaurantDao
    {
        Task<string> UpsertAsync(RestaurantDto restaurantDto);

        Task<IEnumerable<RestaurantDto>> GetAsync();
        Task<RestaurantDto> GetAsync(string restaurantId);

        Task<RestaurantDto> DeleteAsync(string restaurantId);
    }
}