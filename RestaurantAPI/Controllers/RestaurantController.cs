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

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantApiContext _dContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantApiContext dbContext, IMapper mapper)
        {
            _dContext = dbContext;
            _mapper = mapper;
        }
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dContext
                .Restaurants
                .Include(r=>r.Address)
                .Include(r=>r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return Ok(restaurantsDtos);
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurantById([FromRoute]int restaurantId)
        {
            var restaurant = _dContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r=>r.Id == restaurantId);

            if (restaurant is null)
                return NotFound();

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return Ok(restaurantDto);
        }
    }
}
