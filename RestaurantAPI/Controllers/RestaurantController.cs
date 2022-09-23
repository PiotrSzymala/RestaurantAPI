using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantApiContext _dContext;
        public RestaurantController(RestaurantApiContext dbContext)
        {
            _dContext = dbContext;
        }
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _dContext
                .Restaurants
                .ToList();

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurantById([FromRoute]int restaurantId)
        {
            var restaurants = _dContext
                .Restaurants.FirstOrDefault(r=>r.Id == restaurantId);

            if (restaurants is null)
                return NotFound();

            return Ok(restaurants);
        }
    }
}
