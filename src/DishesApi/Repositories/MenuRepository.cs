using System.Collections.Generic;
using System.Threading.Tasks;
using DishesApi.Entities;

namespace DishesApi.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IDishRepository _dishRepository;

        private readonly IRestaurantRepository _restaurantRepository;

        public MenuRepository(IDishRepository dishRepository, IRestaurantRepository restaurantRepository)
        {
            _dishRepository = dishRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Menu> GetAsync(string restaurantId)
        {
            var restaurantTask = _restaurantRepository.GetAsync(restaurantId);
            var dishesTask = _dishRepository.GetAsync(restaurantId);

            var tasks = new List<Task> {restaurantTask, dishesTask};
            await Task.WhenAll(tasks);

            return new Menu
            {
                RestaurantDto = restaurantTask.Result,
                DishDtos = dishesTask.Result
            };
        }
    }
}