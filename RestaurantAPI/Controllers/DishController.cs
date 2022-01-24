﻿using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }


        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            int newDishId=_service.Create(restaurantId, dto);

            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}",null);
        }
    }
}
