using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        public RestaurantDto GetById(int id);

        public IEnumerable<RestaurantDto> GetAll();

        public int CreateRestaurant(CreateRestaurantDto dto);

        public void Delete(int id);

        public void Update(int id, UpdateRestaurantDto dto);
    }
}
