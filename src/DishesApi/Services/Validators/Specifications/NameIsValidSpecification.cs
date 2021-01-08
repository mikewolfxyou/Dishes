using DishesApi.DataAccess;
using DomainValidation.Interfaces.Specification;

namespace DishesApi.Services.Validators.Specifications
{
    public class NameIsValidSpecification<T> : ISpecification<T> where T : IDto
    {
        public const int NameMinLength = 3;
        
        public bool IsSatisfiedBy(T entity)
        {
            return !string.IsNullOrEmpty(entity.Name)
                   && entity.Name.Length > NameMinLength;
        }
    }
}