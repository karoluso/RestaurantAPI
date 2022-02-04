using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class RestaurantServices : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantServices> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantServices> logger,
            IAuthorizationService authorizationService,IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public async Task UpdateAsync(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");


            var authorisationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
                new ResourceOperationRequirement(ResourceOperationEnum.Update));

            if (!authorisationResult.Succeeded)
            {
                throw new NotAuthorizedException();
            }


            restaurant = _mapper.Map(dto, restaurant);

            _dbContext.Restaurants.Update(restaurant);
            _dbContext.SaveChanges();
        }


        public void Delete(int id)
        {
            _logger.LogError($"Restaurnt with id: {id} DELETE action invoked");

            var restaurant = _dbContext.Restaurants
                .Include(r => r.Address)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var authorisationResult =  _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
                new ResourceOperationRequirement(ResourceOperationEnum.Delete)).Result;

            if (!authorisationResult.Succeeded)
            {
                throw new NotAuthorizedException();
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.Addresses.Remove(restaurant.Address);

            _dbContext.SaveChanges();
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(_r => _r.Address)
                .Include(_r => _r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

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

            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantDtos;
        }


        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedByUserId = _userContextService.GetUserId;

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
