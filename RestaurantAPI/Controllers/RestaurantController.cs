﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Controllers
{
    //[ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController( IRestaurantService service)
        {
            _service = service;
        }

        [HttpPost]
       
        public ActionResult CreateRestaurant ([FromBody]CreateRedtaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           int id= _service.CreateRestaurant(dto);

            return Created($"/api/restaruant/{id}", null);
        }


        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            #region alternative 
            //var restaurantDtos = restaurants.Select(r =>  ChangeToDto(r) ); //mapping long way

            //var restaurantDtos = restaurants.Select(r => _mapper.Map<RestaurantDto>(r)); //alternative way to teh below

            //var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            #endregion

            var restaurantDtos = _service.GetAll();

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
            var restaurantDto=_service.GetById(id);

            if (restaurantDto is null)
            {
                return NotFound();
            }
            
            return Ok(restaurantDto);
        }
    }
}
