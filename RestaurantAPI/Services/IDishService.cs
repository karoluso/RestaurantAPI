using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        public int Create(int restaurantId, CreateDishDto dto);

        public IEnumerable<DishDto> GetAll(int restaurantId);

        public DishDto GetById(int restaurantId, int dishId);

        public void DeleteAll(int restaurantId);

        public void DeleteById(int restaurantId, int dishId);
    }
}
