using SpaceshipOtus.Homework2;
using SpaceshipOtus.Homework4;
using static SpaceshipOtusTests.TestCommandsAndMacroCommandsObjects;

namespace SpaceshipOtusTests
{
    public class CommandsAndMacroCommandsTests
    {
        [Fact(DisplayName = "[001] MoveCommand: Update Location Based on Velocity")]
        public void MoveCommandExecute_ValidLocationAndVelocity_UpdatesLocation()
        {
            // Arrange
            var uObject = new MoveMockUObject();
            uObject.SetProperty("Location", new Vector(10, 20));
            uObject.SetProperty("Velocity", new Vector(3, 5));
            var command = new MoveCommand(uObject);

            // Act
            command.Execute();

            // Assert
            var location = (Vector)uObject.GetProperty("Location");
            var velocity = (Vector)uObject.GetProperty("Velocity");
            Assert.Equal(13, location.X);
            Assert.Equal(25, location.Y);
            Assert.Equal(3, velocity.X);
            Assert.Equal(5, velocity.Y);
        }

        [Fact(DisplayName = "[002] RotateCommand: Angle Update Based on AngularVelocity")]
        public void RotateCommandExecute_ValidAngleAndAngularVelocity_UpdatesAngle()
        {
            // Arrange
            var uObject = new RotateMockUObject();
            uObject.SetProperty("Angle", 45);
            uObject.SetProperty("AngularVelocity", 10);
            var command = new RotateCommand(uObject);

            // Act
            command.Execute();

            // Assert
            var newAngle = (int)uObject.GetProperty("Angle");
            Assert.Equal(55, newAngle); 
        }


        [Fact(DisplayName = "[003] CheckFuelCommand: Enough fuel - no exception thrown")]
        public void CheckFuelCommandExecute_EnoughFuel_DoesNotThrow()
        {
            // Arrange
            var uObject = new CheckFuelMockUObject();
            uObject.SetProperty("Fuel", 100);
            uObject.SetProperty("FuelConsumption", 50);
            var command = new CheckFuelCommand(uObject);

            // Act
            var exception = Record.Exception(() => command.Execute());

            // Assert
            Assert.Null(exception);
        }

        [Fact(DisplayName = "[004] CheckFuelCommand: Недостаточно топлива - выбрасывается CommandException")]
        public void CheckFuelCommandExecute_NotEnoughFuel_ThrowsCommandException()
        {
            // Arrange
            var uObject = new CheckFuelMockUObject();
            uObject.SetProperty("Fuel", 50);
            uObject.SetProperty("FuelConsumption", 100);
            var command = new CheckFuelCommand(uObject);

            // Act & Assert
            Assert.Throws<CommandException>(() => command.Execute());
        }

        [Fact(DisplayName = "[005] BurnFuelCommand: Уменьшение топлива на значение FuelConsumption")]
        public void BurnFuelCommandExecute_ReducesFuelByConsumption_Ok()
        {
            // Arrange
            var uObject = new BurnFuelMockUObject();
            uObject.SetProperty("Fuel", 100);
            uObject.SetProperty("FuelConsumption", 30);
            var command = new BurnFuelCommand(uObject);

            command.Execute();

            Assert.Equal(70, uObject.GetProperty("Fuel"));
        }

        [Fact(DisplayName = "[006] ChangeVelocityCommand: Изменение Velocity на основе Angle")]
        public void ChangeVelocityCommandExecute_ChangesVelocityBasedOnAngle_Ok()
        {
            // Arrange
            var uObject = new ChangeVelocityMockUObject();
            var command = new ChangeVelocityCommand(uObject);

            // Act
            command.Execute();
            Vector newVelocity = (Vector)uObject.GetProperty("Velocity");

            // Assert
            Assert.Equal(7, newVelocity.X);
            Assert.Equal(0, newVelocity.Y);
        }

        [Fact(DisplayName = "[007] MacroCommands: Executing a Sequence of Commands")]
        public void MacroCommandsExecute_ExecutesAllCommands()
        {
            // Arrange
            var uObject = new UniversalMockUObject();
            uObject.SetProperty("Location", new Vector(10, 20));
            uObject.SetProperty("Velocity", new Vector(3, 5));
            uObject.SetProperty("Angle", 45);
            uObject.SetProperty("AngularVelocity", 10);

            var commands = new CommandAbstract[]
            {
                new MoveCommand(uObject),
                new RotateCommand(uObject)
            };

            var macroCommand = new MacroCommands(commands);

            // Act
            macroCommand.Execute();

            // Assert
            var location = (Vector)uObject.GetProperty("Location");
            var angle = (int)uObject.GetProperty("Angle");
            Assert.Equal(13, location.X);
            Assert.Equal(25, location.Y);
            Assert.Equal(55, angle);
        }

        [Fact(DisplayName = "[008] MacroCommands: Error in one of the commands - CommandException thrown")]
        public void MacroCommandsExecute_CommandFails_ThrowsCommandException()
        {
            // Arrange
            var uObject = new CheckFuelMockUObject();
            uObject.SetProperty("Fuel", 50);
            uObject.SetProperty("FuelConsumption", 100);

            var commands = new CommandAbstract[]
            {
                new CheckFuelCommand(uObject), // Эта команда выбросит исключение
                new MoveCommand(uObject)
            };

            var macroCommand = new MacroCommands(commands);

            // Act & Assert
            Assert.Throws<CommandException>(() => macroCommand.Execute());
        }

        [Fact(DisplayName = "[009] MoveWithFuelCommand: Execute a sequence of commands (CheckFuel, Move, BurnFuel)")]
        public void MoveWithFuelCommandExecute_ExecutesAllCommands()
        {
            // Arrange
            var uObject = new UniversalMockUObject();
            uObject.SetProperty("Location", new Vector(10, 20));
            uObject.SetProperty("Velocity", new Vector(3, 5));
            uObject.SetProperty("Fuel", 100);
            uObject.SetProperty("FuelConsumption", 10);

            var moveWithFuelCommand = new MoveWithFuelCommand(uObject);

            // Act
            moveWithFuelCommand.Execute();

            // Assert
            var location = (Vector)uObject.GetProperty("Location");
            var fuel = (int)uObject.GetProperty("Fuel");
            Assert.Equal(13, location.X);
            Assert.Equal(25, location.Y);
            Assert.Equal(90, fuel);
        }

        [Fact(DisplayName = "[010] MoveWithFuelCommand: Not enough fuel - throws CommandException")]
        public void MoveWithFuelCommandExecute_NotEnoughFuel_ThrowsCommandException()
        {
            // Arrange
            var uObject = new UniversalMockUObject();
            uObject.SetProperty("Location", new Vector(10, 20));
            uObject.SetProperty("Velocity", new Vector(3, 5));
            uObject.SetProperty("Fuel", 5); // Недостаточно топлива
            uObject.SetProperty("FuelConsumption", 10);

            var moveWithFuelCommand = new MoveWithFuelCommand(uObject);

            // Act & Assert
            Assert.Throws<CommandException>(() => moveWithFuelCommand.Execute());
        }

        [Fact(DisplayName = "[011] RotateWithVelocityCommand: Execute a sequence of commands (Rotate, ChangeVelocity)")]
        public void RotateWithVelocityCommandExecute_ExecutesAllCommands()
        {
            // Arrange
            var uObject = new UniversalMockUObject();
            uObject.SetProperty("Angle", 45);
            uObject.SetProperty("AngularVelocity", 10);
            uObject.SetProperty("Velocity", new Vector(10, 0));

            var rotateWithVelocityCommand = new RotateWithVelocityCommand(uObject);

            // Act
            rotateWithVelocityCommand.Execute();

            // Assert
            var angle = (int)uObject.GetProperty("Angle");
            var velocity = (Vector)uObject.GetProperty("Velocity");
            Assert.Equal(55, angle);
            Assert.Equal(5, velocity.X);
            Assert.Equal(0, velocity.Y);
        }
    }
}
