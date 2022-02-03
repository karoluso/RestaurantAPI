﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
      

        public RestaurantController(IRestaurantService service)
        {
            _restaurantService = service;
          
        }


        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {

            _restaurantService.UpdateAsync(id, dto, User);  // User from ClaimPrincipals

            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id, User);

            return NoContent();
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);   //check for null

            int id = _restaurantService.CreateRestaurant(dto,userId);

            return Created($"/api/restaruant/{id}", null);
        }


        [HttpGet]
        //[Authorize(Policy ="HasNationality")]
        [Authorize(Policy = "HasMinimumAge20")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            #region alternative 
            //var restaurantDtos = restaurants.Select(r =>  ChangeToDto(r) ); //mapping long way

            //var restaurantDtos = restaurants.Select(r => _mapper.Map<RestaurantDto>(r)); //alternative way to teh below

            //var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            #endregion

            var restaurantDtos = _restaurantService.GetAll();

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
        [AllowAnonymous]
        public ActionResult<RestaurantDto> GetOne([FromRoute] int id)
        {
            var restaurantDto = _restaurantService.GetById(id);

            return Ok(restaurantDto);
        }
    }
}
