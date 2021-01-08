using System.Collections.Generic;
using System.Threading.Tasks;
using DishesApi.DataAccess.Restaurant;

namespace DishesApi.Repositories
{
    public interface IRestaurantRepository
    {
        Task<string> UpsertAsync(RestaurantDto restaurantDto);

        Task<IEnumerable<RestaurantDto>> GetAsync();
        Task<RestaurantDto> GetAsync(string restaurantId);

        Task<bool> DeleteAsync(string restaurantId);
    }
}