using System.Text.RegularExpressions;
using DishesApi.DataAccess.Dish;
using DomainValidation.Interfaces.Specification;

namespace DishesApi.Services.Validators.DishSpecifications
{
    public class RestaurantIdIsValidSpecification : ISpecification<DishDto>
    {
        public bool IsSatisfiedBy(DishDto entity)
        {
            return !string.IsNullOrEmpty(entity.RestaurantId) && 
                Regex.Match(entity.RestaurantId, 
                @"^[a-f\d]{24}$", 
                RegexOptions.IgnoreCase
            ).Success;
        }
    }
}