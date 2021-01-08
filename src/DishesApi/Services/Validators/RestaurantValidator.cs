using DishesApi.DataAccess.Restaurant;
using DishesApi.Services.Validators.RestaurantSpecifications;
using DishesApi.Services.Validators.Specifications;
using DomainValidation.Validation;

namespace DishesApi.Services.Validators
{
    public sealed class RestaurantValidator : Validator<RestaurantDto>
    {
        public RestaurantValidator()
        {
            
            Add("NameIsValid", new Rule<RestaurantDto>(new NameIsValidSpecification<RestaurantDto>(),
                "Restaurant name is invalid, must more then " 
                + NameIsValidSpecification<RestaurantDto>.NameMinLength + " characters"));
            
            Add("CountryIsValid", new Rule<RestaurantDto>(new CountryIsValidSpecification(),
                "Country has to be one country of the countries list"
                ));
            
            Add("CityIsValid", new Rule<RestaurantDto>(new CityIsValidSpecification(),
                "City is invalid, must more then " 
                + CityIsValidSpecification.CityMinLength + " characters"));
        }
    }
}