using System.Globalization;
using System.Text.RegularExpressions;
using DishesApi.DataAccess.Dish;
using DomainValidation.Interfaces.Specification;

namespace DishesApi.Services.Validators.DishSpecifications
{
    public class PriceIsValidSpecification : ISpecification<DishDto>
    {
        public bool IsSatisfiedBy(DishDto entity)
        {
            return Regex.Match(entity.Price.ToString(CultureInfo.CurrentCulture), 
                @"^\d+(\.|,)?\d{2}$", 
                RegexOptions.IgnoreCase
                ).Success;
        }
    }
}