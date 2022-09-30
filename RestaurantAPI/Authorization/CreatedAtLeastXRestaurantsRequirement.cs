using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedAtLeastXRestaurantsRequirement : IAuthorizationRequirement
    {
        public int NumberOfRestaurants { get; set; }

        public CreatedAtLeastXRestaurantsRequirement(int numberOfRestaurants)
        {
            NumberOfRestaurants = numberOfRestaurants;
        }
    }
}
