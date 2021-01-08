using System.Collections;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Entities;
using DishesApi.Services.Validators.RestaurantSpecifications;
using NUnit.Framework;

namespace DishApi.Tests.Services.Validators.RestaurantValidSpecifications
{
    public class CountryIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(CountryIsValidTestCases))]
        public void Should_Satisfied_When_CountryIsValid(
            RestaurantDto dto,
            bool expectResult
        )
        {
            var countryIsValidSpecification = new CountryIsValidSpecification();
            
            Assert.AreEqual(expectResult, countryIsValidSpecification.IsSatisfiedBy(dto));
        }
        
        private class CountryIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new RestaurantDto(),
                    false
                ).SetName("Should_Not_Satisfied_When_Country_Name_Is_Null");

                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        Country = Countries.Germany
                    },
                    true
                ).SetName("Should_Not_Satisfied_When_Country_Name_Is_Not_DACH");
            }
        }
    }
}