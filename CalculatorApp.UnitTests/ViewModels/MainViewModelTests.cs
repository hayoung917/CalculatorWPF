using System;
using System.Collections.Generic;
using System.Text;
using CalculatorApp.ViewModels;
using FluentAssertions;
using Saige.MVVM;
using Xunit;

namespace CalculatorApp.UnitTests.ViewModels
{
    public class MainViewModelTests
    {
        //command test할것
        //생성자 test할것
        #region Constructor
        [Fact]
        public void Constructor_does_construct()
        {
            //Arrange

            //Act
            var sut = new Action(() => new MainViewModel());

            //Assert
            sut.Should().NotThrow();
        }
        #endregion

        #region command
        [Theory]
        [InlineData("1","1")]
        [InlineData("1258", "1258")]
        [InlineData("asdf", "asdf")]
        public void InputCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            var sut = new MainViewModel();

            //Act
            sut.InputCommand.Execute(commandParameter);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", " ")]
        [InlineData("1258", " ")]
        [InlineData("asdf", " ")]
        [InlineData("", " ")]
        public void ClearCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            var sut = new MainViewModel();

            //Act
            sut.ClearCommand.Execute(commandParameter);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", " ")]
        [InlineData("1258", "125")]
        [InlineData("asdf", "asd")]
        [InlineData(" ", " ")]
        [InlineData("", " ")]
        [InlineData("hylee", "hyle")]
        public void BackspaceCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            var sut = new MainViewModel();
            sut.NumOutput = commandParameter;
            //Act
            sut.BackspaceCommand.Execute(sut.NumOutput);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }
        #endregion command


    }
}
