using System.Collections;
using DishesApi.DataAccess.Dish;
using DishesApi.Services.Validators.DishSpecifications;
using NUnit.Framework;

namespace DishApi.Tests.Services.Validators.DishSpecifications
{
    public class AwaitingTimeIsValidSpecificationTest
    {
        [Test]
        [TestCaseSource(typeof(AwaitingTimeIsValidTestCases))]
        public void Should_Satisfied_When_AwaitingTimeIsValid(
            DishDto dto,
            bool expectResult
        )
        {
            var awaitingTimeIsValidSpecification = new AwaitingTimeIsValidSpecification();

            Assert.AreEqual(expectResult, awaitingTimeIsValidSpecification.IsSatisfiedBy(dto));
        }

        private class AwaitingTimeIsValidTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        AwaitingTime = -1
                    },
                    false
                ).SetName("Should_NOT_Satisfied_When_Awaiting_Time_Is_Minus");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        AwaitingTime = 1111
                    },
                    false
                ).SetName("Should_NOT_Satisfied_When_Awaiting_Time_More_Than_Three_Digits");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        AwaitingTime = 11
                    },
                    true
                ).SetName("Should_Satisfied_When_Awaiting_Time_Is_Two_Digits");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        AwaitingTime = 0
                    },
                    true
                ).SetName("Should_Satisfied_When_Awaiting_Time_Is_One_Digits");
                
                yield return new TestCaseData(
                    new DishDto
                    {
                        AwaitingTime = 111
                    },
                    true
                ).SetName("Should_Satisfied_When_Awaiting_Time_Is_Three_Digits");
            }
        }
    }
}