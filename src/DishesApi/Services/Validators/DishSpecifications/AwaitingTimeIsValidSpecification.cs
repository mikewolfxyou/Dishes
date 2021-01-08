using System.Text.RegularExpressions;
using DishesApi.DataAccess.Dish;
using DomainValidation.Interfaces.Specification;

namespace DishesApi.Services.Validators.DishSpecifications
{
    public class AwaitingTimeIsValidSpecification : ISpecification<DishDto>
    {
        public bool IsSatisfiedBy(DishDto entity)
        {
            return Regex.Match(entity.AwaitingTime.ToString(), 
                @"^\d{1,3}$", 
                RegexOptions.None
            ).Success;
        }
    }
}