using System.Collections.Generic;
using DishesApi.DataAccess.Dish;
using DishesApi.DataAccess.Restaurant;

namespace DishesApi.Entities
{
    public class Menu
    {
        public RestaurantDto RestaurantDto;

        public IEnumerable<DishDto> DishDtos;
    }
}