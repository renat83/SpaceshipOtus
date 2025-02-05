using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SpaceshipOtus.Homework2;

namespace SpaceshipOtusTests
{
    public class MoveTests
    {
        [Fact(DisplayName = "[001]Checking the change in the position of an object")]
        public void Move_ChangesLocationCorrectly_OK()
        {
            // Arrange
            var testObject = new TestObject();
            testObject.SetProperty("Location", new Vector(12, 5)); // Начальное положение
            testObject.SetProperty("Velocity", new Vector(-7, 3)); // Скорость

            var adapter = new MovingObjectAdapter(testObject);
            var move = new Move(adapter);

            // Act
            move.Execute();

            // Assert
            var newLocation = (Vector)testObject.GetProperty("Location");
            Assert.Equal(5, newLocation.X);
            Assert.Equal(8, newLocation.Y);
        }

        [Fact(DisplayName = "[002] Move an object whose position cannot be read, приводит к ошибке")]
        // Скорость задана, положение не задано
        public void Move_ThrowsWhenLocationCannotBeRead_Exception()
        {
            // Arrange
            var testObject = new TestObject();
            testObject.SetProperty("Velocity", new Vector(-7, 3)); 

            var adapter = new MovingObjectAdapter(testObject);
            var move = new Move(adapter);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => move.Execute());
            Assert.Contains("Location", exception.Message);
        }

        [Fact(DisplayName = "[003] Move an object that cannot have its speed read will result in an error")]
        // Положение задано, скорость не задана
        public void Move_ThrowsWhenVelocityCannotBeRead_Exception()
        {
            // Arrange
            var testObject = new TestObject();
            testObject.SetProperty("Location", new Vector(12, 5)); 

            var adapter = new MovingObjectAdapter(testObject);
            var move = new Move(adapter);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => move.Execute());
            Assert.Contains("Velocity", exception.Message); // Проверяем, что ошибка связана со скоростью
        }

        [Fact(DisplayName = "[004] Move an object whose position cannot be changed results in an error")]
        public void Move_ThrowsWhenLocationCannotBeSet_Exception()
        {
            // Arrange
            var testObject = new TestMoveObjectWithSetLocationFailure();
            testObject.SetProperty("Location", new Vector(12, 5));
            testObject.SetProperty("Velocity", new Vector(-7, 3)); 

            // Запрещаем изменение Location
            testObject.DisallowSettingLocation();

            var adapter = new MovingObjectAdapter(testObject);
            var move = new Move(adapter);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => move.Execute());
            Assert.Contains("Location cannot be set", exception.Message);
        }
    }
}
