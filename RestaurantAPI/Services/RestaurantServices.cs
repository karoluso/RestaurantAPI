using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Services
{
    public class RestaurantServices :IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantServices> _logger;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper,ILogger<RestaurantServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public bool Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant=_dbContext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null) return false;

            restaurant = _mapper.Map(dto, restaurant);
                                                                    
            _dbContext.Restaurants.Update(restaurant);
            _dbContext.SaveChanges();

            return true;
        }


        public bool Delete(int id)
        {
            _logger.LogError($"Restaurnt with id: {id} DELETE action invoked");

            var restaurant=_dbContext.Restaurants
                .Include(r=>r.Address)
                .FirstOrDefault(r=>r.Id == id);

            if (restaurant == null) return false;

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.Addresses.Remove(restaurant.Address);

            _dbContext.SaveChanges();

            return true;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(_r => _r.Address)
                .Include(_r => _r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null) return null;

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var  restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantDtos;
        }


        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
