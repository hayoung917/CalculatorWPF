using System;
using CalculatorApp.Models;
using CalculatorApp.ViewModels;
using FluentAssertions;
using Xunit;
using Moq;
using CalculatorApp.Service;
using System.Runtime.CompilerServices;

namespace CalculatorApp.UnitTests.ViewModels
{
    public class MainViewModelTests
    {
        #region Constructor
        [Fact]
        public void Constructor_does_construct()
        {
            //Arrange 
            Mock<IMessageService> stubService = new Mock<IMessageService>();

            //Act
            //var creating = new Action(() => new MainViewModel(stubService.Object));
            var sut = new MainViewModel(stubService.Object);

            //Assert
            //sut.Should().NotThrow();
            sut.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_does_throw_argumentnullexeption_if_parameter_is_null()
        {
            //Arrange 
            //Act
            var sut = new Action(() => new MainViewModel(null));

            //Assert
            sut.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region command
        [Theory]
        [InlineData("1", "1")]
        [InlineData("1258", "1258")]
        [InlineData("asdf", " ")]
        [InlineData("0.", "0.")]
        [InlineData(".5", ".5")]
        [InlineData("..0", " ")]
        [InlineData(null, " ")]
        [InlineData("..555..", " ")]
        [InlineData("hu8hgygyugt'';;[][]yu", " ")]
        [InlineData("-99", " ")]
        public void Excute_of_InputCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            Mock<IMessageService> stubService = new Mock<IMessageService>();
            var sut = new MainViewModel(stubService.Object);

            //Act
            sut.InputCommand.Execute(commandParameter);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", " ")]
        //[InlineData("1258", " ")]
        //[InlineData("asdf", " ")]
        //[InlineData("", " ")]
        //[InlineData("2.586", " ")]
        //[InlineData(null, " ")]
        public void Excute_of_ClearCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            Mock<IMessageService> mockService = new Mock<IMessageService>();
            
            //mockService.Setup(x => x.ShowMessage(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var sut = new MainViewModel(mockService.Object);

            //Act
            sut.ClearCommand.Execute(commandParameter);

            //Assert
            mockService.Verify(q => q.ShowMessage(It.IsAny<string>(), It.IsAny<string>()),Times.Exactly(5));
        }

        [Theory]
        [InlineData("1", " ")]
        [InlineData("1258", "125")]
        [InlineData("12.", "12")]
        [InlineData("asdf", "asd")]
        [InlineData(" ", " ")]
        [InlineData("", " ")]
        [InlineData("hylee", "hyle")]
        [InlineData(null, " ")]
        public void Excute_of_BackspaceCommand_does_return_correctly(
            string commandParameter, string expectedResult)
        {
            //Arrange
            Mock<IMessageService> stubService = new Mock<IMessageService>();
            var sut = new MainViewModel(stubService.Object);
            sut.NumOutput = commandParameter;
            //Act
            sut.BackspaceCommand.Execute(sut.NumOutput);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }
        #endregion command

        [Fact]
        public void EnumUnits_does_return_EnumUnits()
        {
            //Arrange
            Mock<IMessageService> stubService = new Mock<IMessageService>();
            var sut = new MainViewModel(stubService.Object);
            var expectedUnit = Enum.GetValues(typeof(TimeUnit));

            //Act
            var result = sut.Units;

            //Assert
            result.Should().BeEquivalentTo(expectedUnit);
        }
    }
}
