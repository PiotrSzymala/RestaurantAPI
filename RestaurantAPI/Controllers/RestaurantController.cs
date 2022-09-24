using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
       
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurants/{id}", null);
        }
       
        //[HttpPost("update")]
        //public ActionResult UpdateRestaurantPhone()
        //{
        //    var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == 16);

            
        //         restaurant.ContactNumber = "987-342-667";
        //         _dbContext.SaveChanges();
            
        //    return Ok();
        //}
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int restaurantId)
        {
            var restaurant = _restaurantService.GetById(restaurantId);

            if (restaurant is null)
                return NotFound();
            
            return Ok(restaurant);
        }
    }
}
