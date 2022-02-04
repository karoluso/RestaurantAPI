using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantController(IRestaurantService service, IHttpContextAccessor httpContextAccessor)
        {
            _restaurantService = service;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {

          await  _restaurantService.UpdateAsync(id, dto); 

            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

            return NoContent();
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
           

            int id = _restaurantService.CreateRestaurant(dto);

            return Created($"/api/restaruant/{id}", null);
        }


        [HttpGet]
        //[Authorize(Policy ="HasNationality")]
        //[Authorize(Policy = "HasMinimumAge20")]
        [Authorize(Policy = "CreatedMinimum2Restaurants")]
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
