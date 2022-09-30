using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedAtLeastXRestaurantsRequirementHandler : AuthorizationHandler<CreatedAtLeastXRestaurantsRequirement>
    {
        private readonly RestaurantApiContext _context;

        public CreatedAtLeastXRestaurantsRequirementHandler(RestaurantApiContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedAtLeastXRestaurantsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCount = _context
                 .Restaurants.Count(r => r.CreatedById == userId);

            if (createdRestaurantsCount >= requirement.NumberOfRestaurants)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
