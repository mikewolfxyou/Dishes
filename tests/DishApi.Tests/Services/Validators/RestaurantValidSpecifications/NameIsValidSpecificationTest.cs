using System.Collections;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Services.Validators.RestaurantSpecifications;
using DishesApi.Services.Validators.Specifications;
using NUnit.Framework;

namespace DishApi.Tests.Services.Validators.RestaurantValidSpecifications
{
    public class NameIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(NameIsValidTestCases))]
        public void Should_Satisfied_When_NameIsValid(
            RestaurantDto dto,
            bool expectResult
            )
        {
            var nameIsValidSpecification = new NameIsValidSpecification<RestaurantDto>();
            
            Assert.AreEqual(expectResult, nameIsValidSpecification.IsSatisfiedBy(dto));
        }
        
        private class NameIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        Name = ""
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_Name_Empty");
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        Name = null
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_Name_Null");
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        Name = "a"
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_Name_Length_Shorter_Than_3");
                
                yield return new TestCaseData(
                    new RestaurantDto
                    {
                        Name = "abc"
                    },
                    false
                ).SetName("Should_Satisfied_When_Name_Is_Valid");
            }
        }
    }
}