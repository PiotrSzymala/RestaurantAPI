using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DateTime = System.DateTime;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var userDateOfBirthClaim = context.User.FindFirst(c => c.Type == "DateOfBirth");
            if (userDateOfBirthClaim == null)
            {
                return Task.CompletedTask;
            }
            var dateOfBirth = DateTime.Parse(userDateOfBirthClaim.Value);

            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User: {userEmail} with date of birth: [{dateOfBirth}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }
            
            return Task.CompletedTask;
        }
    }
}
