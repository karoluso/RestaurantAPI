using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbcontext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }


        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = _dbcontext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var dishEntity = _mapper.Map<Dish>(dto);

            dishEntity.RestaurantId = restaurantId;

            _dbcontext.Dishes.Add(dishEntity);
            _dbcontext.SaveChanges();

            return dishEntity.Id;
        }


        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = _dbcontext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");


            var dishes = restaurant.Dishes;

            //List<DishDto> dishDtos = dishes.Select(r=> _mapper.Map<DishDto> (r)).ToList();

            List<DishDto> dishDtos = _mapper.Map<List<DishDto>>(dishes);

            return dishDtos;
        }


        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant= _dbcontext.Restaurants
                .Include(r=>r.Dishes)
                .FirstOrDefault(r=>r.Id== restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var dish = restaurant.Dishes.FirstOrDefault(d=>d.Id==dishId);

            if (dish == null)
                throw new NotFoundException("Dish not found");

            var dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }
    }
}
