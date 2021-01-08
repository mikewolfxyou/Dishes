using DishesApi.DataAccess.Restaurant;
using DomainValidation.Interfaces.Specification;

namespace DishesApi.Services.Validators.RestaurantSpecifications
{
    public class CityIsValidSpecification : ISpecification<RestaurantDto>
    {
        public const int CityMinLength = 3;
        
        public bool IsSatisfiedBy(RestaurantDto entity)
        {
            return !string.IsNullOrEmpty(entity.City)
                   && entity.City.Length > CityMinLength;
        }
    }
}