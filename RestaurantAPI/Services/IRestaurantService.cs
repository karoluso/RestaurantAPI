using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        public RestaurantDto GetById(int id);

        public IEnumerable<RestaurantDto> GetAll();

        public int CreateRestaurant(CreateRedtaurantDto dto);
        public bool  Delete(int id);
    }
}
