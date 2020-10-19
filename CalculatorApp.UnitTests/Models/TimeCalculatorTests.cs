using System.Collections;
using System.Collections.Generic;
using CalculatorApp.Models;
using FluentAssertions;
using Xunit;

namespace CalculatorApp.UnitTests.Models
{
    public class TimeCalculatorTests
    {
        #region GetAllConvertedTimes
        public class TestSet_GetAllConvertedTimes_does_return_correctly : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    1, TimeUnit.Microsecond, 
                    new List<Time>{
                        new Time { Unit = TimeUnit.Microsecond, Value = 1},
                        new Time { Unit = TimeUnit.Millisecond, Value = 0.001},
                        new Time { Unit = TimeUnit.Second, Value = 0},
                    }
                };

                yield return new object[] {
                    0.3, TimeUnit.Millisecond,
                    new List<Time>{
                        new Time { Unit = TimeUnit.Microsecond, Value = 300},
                        new Time { Unit = TimeUnit.Millisecond, Value = 0.3},
                        new Time { Unit = TimeUnit.Second, Value = 0},
                    }
                };

                yield return new object[] {
                    900, TimeUnit.Second,
                    new List<Time>{
                        new Time { Unit = TimeUnit.Microsecond, Value = 900000000},
                        new Time { Unit = TimeUnit.Millisecond, Value = 900000},
                        new Time { Unit = TimeUnit.Second, Value = 900},
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestSet_GetAllConvertedTimes_does_return_correctly))]
        public void GetAllConvertedTimes_does_return_correctly(
            double input, TimeUnit baseUnit, List<Time> expectedConvertedTimes)
        {
            //Arrange
            //Act
            var convertedTimes = TimeCalculator.GetAllConvertedTimes(input, baseUnit);

            //Assert
            convertedTimes.Should().BeEquivalentTo(expectedConvertedTimes);
        } 
        #endregion
    }
}
