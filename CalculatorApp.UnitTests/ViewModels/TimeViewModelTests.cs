using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using CalculatorApp.Models;
using CalculatorApp.ViewModels;
using FluentAssertions;
using Xunit;

namespace CalculatorApp.UnitTests.ViewModels
{
    public class TimeViewModelTests
    {
        #region ConvertedTimeViewModel
        public class TestSet_CovertedTimeViewModel_does_return_correctly : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    1, TimeUnit.Microsecond,
                    new List<TimeViewModel>{
                        new TimeViewModel(new Time { Unit = TimeUnit.Microsecond, Value = 1}),
                        new TimeViewModel(new Time { Unit = TimeUnit.Millisecond, Value = 0.001}),
                        new TimeViewModel(new Time { Unit = TimeUnit.Second, Value = 0}),
                    }
                };

                yield return new object[] {
                    1.5, TimeUnit.Millisecond,
                    new List<TimeViewModel>{
                        new TimeViewModel(new Time { Unit = TimeUnit.Microsecond, Value = 1500}),
                        new TimeViewModel(new Time { Unit = TimeUnit.Millisecond, Value = 1.5}),
                        new TimeViewModel(new Time { Unit = TimeUnit.Second, Value = 0.002}),
                    }
                };

                yield return new object[] {
                    8.2, TimeUnit.Second,
                    new List<TimeViewModel>{
                        new TimeViewModel(new Time { Unit = TimeUnit.Microsecond, Value = 8200000}),
                        new TimeViewModel(new Time { Unit = TimeUnit.Millisecond, Value = 8200}),
                        new TimeViewModel(new Time { Unit = TimeUnit.Second, Value = 8.2}),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(TestSet_CovertedTimeViewModel_does_return_correctly))]
        public void CovertedTimeViewModel_does_return_correctly(
            double input, TimeUnit baseUnit, List<TimeViewModel> expectedConvertedModels)
        {
            //Arrange
            //Act
            var convertedModels = TimeViewModel.CovertedTimeViewModel(input, baseUnit);

            //Assert
            convertedModels.Should().BeEquivalentTo(expectedConvertedModels);
        }
        #endregion

        #region TimeViewModel
        [Theory]
        [InlineData(5,TimeUnit.Microsecond)]
        [InlineData(3.8, TimeUnit.Millisecond)]
        [InlineData(30000, TimeUnit.Second)]
        public void Constructor_does_coustruct_correctly(
            double expectedValue, TimeUnit expectedUnit)
        {
            //Arrange
            var time = new Time { Value = expectedValue, Unit = expectedUnit };

            //Act
            var sut = new TimeViewModel(time);

            //Assert
            sut.Value.Should().Be(expectedValue);
            sut.Unit.Should().Be(expectedUnit);
        }

        [Fact]
        public void Constructor_does_coustruct_correctly_if_time_is_null()
        {
            //Arrange\
            //Act
            var sut = new TimeViewModel(null);

            //Assert
            sut.Value.Should().Be(0);
            sut.Unit.Should().Be((TimeUnit)default);
        }
        #endregion
    }
}
