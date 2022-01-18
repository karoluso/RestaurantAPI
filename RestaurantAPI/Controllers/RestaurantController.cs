using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateRestaurant ([FromBody]CreateRedtaurantDto dto)
        {
            var restaurant =_mapper.Map<Restaurant>(dto);

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return Created($"/api/restaruant/{restaurant.Id}", null);
        }


        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r=>r.Address)
                .Include(r=>r.Dishes)
                .ToList();

            //var restaurantDtos = restaurants.Select(r =>  ChangeToDto(r) ); //mapping long way

            //var restaurantDtos = restaurants.Select(r => _mapper.Map<RestaurantDto>(r));

            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return Ok(restaurantDtos);
        }

       // private RestaurantDto ChangeToDto(Restaurant r)
        //{
        //    var dto = new RestaurantDto()
        //    {
        //      Id = r.Id,
        //        Name = r.Name,
        //        Description = r.Description,
        //       Category = r.Category
        //    };
        //    return dto;
        //}

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetOne(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(_r => _r.Address)
                .Include(_r => _r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            
            return Ok(restaurantDto);
        }
    }
}
