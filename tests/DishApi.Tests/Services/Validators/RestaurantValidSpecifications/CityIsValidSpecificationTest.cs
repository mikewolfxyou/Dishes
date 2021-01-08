using System.Collections;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Services.Validators.RestaurantSpecifications;
using NUnit.Framework;

namespace DishApi.Tests.Services.Validators.RestaurantValidSpecifications
{
    public class CityIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(CityIsValidTestCases))]
        public void Should_Satisfied_When_CityIsValid(
            RestaurantDto dto,
            bool expectResult
            )
        {
            var cityIsValidSpecification = new CityIsValidSpecification();
            
            Assert.AreEqual(expectResult, cityIsValidSpecification.IsSatisfiedBy(dto));
        }
        
        private class CityIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        City = ""
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_City_Empty");
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        City = null
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_City_Null");
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        City = "a"
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_City_Length_Shorter_Than_3");
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        City = "abc"
                    },
                    false
                ).SetName("Should_Satisfied_When_City_Is_Valid");
            }
        }
    }
}