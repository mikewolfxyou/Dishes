using System.Collections.Generic;
using System.Linq;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Entities;
using DomainValidation.Interfaces.Specification;

namespace DishesApi.Services.Validators.RestaurantSpecifications
{
    public class CountryIsValidSpecification : ISpecification<RestaurantDto>
    {
        private readonly List<Countries> _validCountry = new List<Countries>
        {
            Countries.Germany,
            Countries.Austria,
            Countries.Swiss
        };

        public bool IsSatisfiedBy(RestaurantDto entity)
        {
            var a = !string.IsNullOrEmpty(entity.Country.ToString());
            return !string.IsNullOrEmpty(entity.Country.ToString()) 
                   && _validCountry.Any(c => c == entity.Country);
        }
    }
}