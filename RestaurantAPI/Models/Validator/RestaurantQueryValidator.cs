using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Models.Validator
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private readonly int[] _allowedPageSizes = new[] { 5, 10, 15 };
        private string[] _sortByColumnNames = new[] { nameof(Restaurant.Name), nameof(Restaurant.Category), nameof(Restaurant.Description)};
        public RestaurantQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!_allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",",_allowedPageSizes)}]");
                }
            });
            RuleFor(r => r.SortBy).Must(value=>string.IsNullOrEmpty(value) || _sortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _sortByColumnNames)}]");
        }
    }
}
