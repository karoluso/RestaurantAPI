using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        public RestaurantDto GetById(int id);

        public IEnumerable<RestaurantDto> GetAll();

        public int CreateRestaurant(CreateRestaurantDto dto, int userId);

        public void Delete(int id, ClaimsPrincipal user);

        public Task UpdateAsync(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
    }
}
