using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        [HttpPost("{restaurantId}")]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] DishDto dto)
        {
            var dish=_mapper
        }
    }
}
