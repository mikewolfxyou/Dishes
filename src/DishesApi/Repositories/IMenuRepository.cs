using System.Threading.Tasks;
using DishesApi.Entities;

namespace DishesApi.Repositories
{
    public interface IMenuRepository
    {
        Task<Menu> GetAsync(string restaurantId);
    }
}