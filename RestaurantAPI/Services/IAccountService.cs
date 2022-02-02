using RestaurantAPI.Controllers;
using RestaurantAPI.Models;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        public Task RegisterUser(RegisterUserDto dto);
        public Task<string> GenerateJwt(LoginDto dto);
    }
}