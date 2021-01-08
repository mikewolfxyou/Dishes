using DishesApi.DataAccess.Dish;
using DishesApi.Services.Validators.DishSpecifications;
using DishesApi.Services.Validators.Specifications;
using DomainValidation.Validation;

namespace DishesApi.Services.Validators
{
    public sealed class DishValidator : Validator<DishDto>
    {
        public DishValidator()
        {
            Add("NameIsValid", new Rule<DishDto>(new NameIsValidSpecification<DishDto>(),
                "Dish name is invalid, must more then " 
                + NameIsValidSpecification<DishDto>.NameMinLength + " characters"));
            Add("PriceIsValid", new Rule<DishDto>(new PriceIsValidSpecification(), 
                "Price is invalid"));

            Add("AwaitingTimeIsValid", new Rule<DishDto>(new AwaitingTimeIsValidSpecification(),
                "Awaiting time is invalid"));
            
            Add("RestaurantIdIsValid", new Rule<DishDto>(new RestaurantIdIsValidSpecification(),
                "Restaurant id is invalid, must be hexadecimal"));
        }
    }
}