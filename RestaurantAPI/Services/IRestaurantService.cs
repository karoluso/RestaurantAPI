using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        public RestaurantDto GetById(int id);

        public PageResult<RestaurantDto> GetAll(RestaurantQuery restaurant);

        public int CreateRestaurant(CreateRestaurantDto dto);

        public void Delete(int id);

        public Task UpdateAsync(int id, UpdateRestaurantDto dto);
    }
}
