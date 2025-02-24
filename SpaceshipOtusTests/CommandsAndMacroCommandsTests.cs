using Microsoft.AspNetCore.Routing;
using SpaceshipOtus.Homework2;
using SpaceshipOtus.Homework4;
using static SpaceshipOtusTests.TestCommandsAndMacroCommandsObjects;

namespace SpaceshipOtusTests
{
    public class CommandsAndMacroCommandsTests
    {
        [Fact]
        public void Execute_ValidLocationAndVelocity_DoesNotThrow()
        {
            // Arrange
            var uObject = new MoveMockUObject();
            var command = new MoveCommand(uObject);

            // Act
            var exception = Record.Exception(() => command.Execute());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Execute_ValidAngleAndAngularVelocity_DoesNotThrow()
        {
            // Arrange
            var uObject = new RotateMockUObject();
            var command = new RotateCommand(uObject);

            // Act
            var exception = Record.Exception(() => command.Execute());

            // Assert
            Assert.Null(exception);
        }


        [Fact]
        public void Execute_EnoughFuel_DoesNotThrow()
        {
            // Arrange
            var uObject = new CheckFuelMockUObject();
            var command = new CheckFuelCommand(uObject);

            // Act
            var exception = Record.Exception(() => command.Execute());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Execute_NotEnoughFuel_ThrowsCommandException()
        {
            // Arrange
            var uObject = new CheckFuelMockUObject();
            uObject.SetProperty("Fuel", 30); // Меняем значение топлива
            var command = new CheckFuelCommand(uObject);

            // Act & Assert
            Assert.Throws<CommandException>(() => command.Execute());
        }

        [Fact]
        public void Execute_ReducesFuelByConsumption()
        {
            // Arrange
            var uObject = new BurnFuelMockUObject();
            var command = new BurnFuelCommand(uObject);

            command.Execute();

            Assert.Equal(50, uObject.GetProperty("Fuel"));
        }

        [Fact]
        public void Execute_ChangesVelocityBasedOnAngle()
        {
            // Arrange
            var uObject = new ChangeVelocityMockUObject();
            var command = new ChangeVelocityCommand(uObject);

            // Act
            command.Execute();
            Vector newVelocity = (Vector)uObject.GetProperty("Velocity");

            // Assert
            Assert.Equal(7, newVelocity.X); // 10 * cos(45°) ≈ 7
            Assert.Equal(0, newVelocity.Y); // 10 * sin(45°) ≈ 7
        }
    }
}
