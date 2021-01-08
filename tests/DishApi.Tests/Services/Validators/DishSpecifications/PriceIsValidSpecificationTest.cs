using System.Collections;
using DishesApi.DataAccess.Dish;
using DishesApi.Services.Validators.DishSpecifications;
using NUnit.Framework;

namespace DishApi.Tests.Services.Validators.DishSpecifications
{
    public class PriceIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(PriceIsValidTestCases))]
        public void Should_Satisfied_When_PriceIsValid(
            DishDto dto,
            bool expectResult
        )
        {
            var priceIsValidSpecification = new PriceIsValidSpecification();
            
            Assert.AreEqual(expectResult, priceIsValidSpecification.IsSatisfiedBy(dto));
        }
        
        private class PriceIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        Price = -5.0m
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_Price_Minus");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        Price = 5.000m
                    },
                    false
                ).SetName("Should_Not_Satisfied_When_Price_More_Than_2_Decimal");

                yield return new TestCaseData(
                    new DishDto
                    {
                        Price = 5m
                    },
                    false
                ).SetName("Should_NOT_Satisfied_When_Price_Has_Not_Decimal");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        Price = 5.0m
                    },
                    false
                ).SetName("Should_NOT_Satisfied_When_Price_Has_Only_1_Decimal");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        Price = 0.00m
                    },
                    true
                ).SetName("Should_Satisfied_When_Price_Zero");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        Price = 0.01m
                    },
                    true
                ).SetName("Should_Satisfied_When_Price_Zero");
            }
        }
    }
}