using System.Collections.Generic;
using System.Threading.Tasks;
using DishesApi.DataAccess.Dish;

namespace DishesApi.Repositories
{
    public interface IDishRepository
    {
        Task<bool> UpsertAsync(DishDto dishDto);

        Task<IEnumerable<DishDto>> GetAsync(IEnumerable<string> dishIds);
        
        Task<IEnumerable<DishDto>> GetAsync(string restaurantId);

        Task<bool> DeleteAsync(IEnumerable<string> dishIds);
    }
}