using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Linq;

namespace RestaurantAPI.Services
{
    public class DishService :IDishService
    {
        private readonly RestaurantDbContext _dbcontext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext,IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto )
        {
            var restaurant=_dbcontext.Restaurants.FirstOrDefault(r=>r.Id==restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

                var dishEntity = _mapper.Map<Dish>(dto);

                dishEntity.RestaurantId = restaurantId;

                _dbcontext.Dishes.Add(dishEntity);
                _dbcontext.SaveChanges();

            return dishEntity.Id;
        }
    }
}
