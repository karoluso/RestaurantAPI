using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        public RestaurantDto GetById(int id);

        public IEnumerable<RestaurantDto> GetAll();

        public int CreateRestaurant(CreateRestaurantDto dto);

        public bool  Delete(int id);

        public bool ModifyRestaurant(int id, ModifyRestaurantDto dto);
    }
}
