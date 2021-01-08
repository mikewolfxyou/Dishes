using System.Collections;
using DishesApi.DataAccess.Dish;
using DishesApi.Services.Validators.DishSpecifications;
using NUnit.Framework;

namespace DishApi.Tests.Services.Validators.DishSpecifications
{
    public class RestaurantIdIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(RestaurantIdIsValidTestCases))]
        public void Should_Satisfied_When_RestaurantIdIsValid(
            DishDto dto,
            bool expectResult
        )
        {
            var validSpecification = new RestaurantIdIsValidSpecification();

            Assert.AreEqual(expectResult, validSpecification.IsSatisfiedBy(dto));
        }

        private class RestaurantIdIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        RestaurantId = ""
                    },
                    false
                ).SetName("Should_NOT_Satisfied_When_RestaurantId_Is_Empty");
                
                yield return new TestCaseData(
                    new DishDto(),
                    false
                ).SetName("Should_NOT_Satisfied_When_RestaurantId_Null");
                
                
                yield return new TestCaseData(
                    new DishDto{
                        RestaurantId = "aa"
                    },
                    false
                ).SetName("Should_NOT_Satisfied_When_RestaurantId_NOT_Hexadecimal");
                
                yield return new TestCaseData(
                    new DishDto{
                        RestaurantId = "45cbc4a0e4123f6920000002"
                    },
                    true
                ).SetName("Should_Satisfied_When_RestaurantId_Is_Hexadecimal");
            }
        }
    }
}