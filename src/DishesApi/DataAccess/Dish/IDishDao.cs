using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DishesApi.DataAccess.Dish
{
    public interface IDishDao
    {
        Task<UpdateResult> UpsertAsync(DishDto dishDto);

        Task<IEnumerable<DishDto>> GetAsync(IEnumerable<string> dishId);
        Task<IEnumerable<DishDto>> GetAsync(string restaurantId);

        Task<DeleteResult> DeleteAsync(IEnumerable<string> dishIds);
    }
}