using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SpaceshipOtus.Homework2;

namespace SpaceshipOtusTests
{
    public class RotateTests
    {
        [Fact(DisplayName = "[001] Checking the change in the angle of an object")]
        public void Rotate_ChangesAngleCorrectly_OK()
        {
            // Arrange
            var testObject = new TestObject();
            testObject.SetProperty("Angle", new Angle(45)); // Начальный угол
            testObject.SetProperty("AngularVelocity", 10); // Угловая скорость

            var adapter = new RotatableAdapter(testObject);
            var rotate = new Rotate(adapter);

            // Act
            rotate.Execute();

            // Assert
            var newAngle = ((Angle)testObject.GetProperty("Angle")).d;
            Assert.Equal(55, newAngle); // 45 + 10 = 55
        }

        [Fact(DisplayName = "[002] Rotate an object whose angle cannot be read results in an error")]
        // Угловая скорость задана, угол не задан
        public void Rotate_ThrowsWhenAngleCannotBeRead_Exception()
        {
            // Arrange
            var testObject = new TestObject();
            testObject.SetProperty("AngularVelocity", 10); 

            var adapter = new RotatableAdapter(testObject);
            var rotate = new Rotate(adapter);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => rotate.Execute());
            Assert.Contains("Angle", exception.Message); // Проверяем, что ошибка связана с углом
        }

        [Fact(DisplayName = "[003] Rotate an object for which angular velocity cannot be read results in an error")]
        // Угол задан, угловая скорость не задана
        public void Rotate_ThrowsWhenAngularVelocityCannotBeRead_Exception()
        {
            // Arrange
            var testObject = new TestObject();
            testObject.SetProperty("Angle", new Angle(45)); 

            var adapter = new RotatableAdapter(testObject);
            var rotate = new Rotate(adapter);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => rotate.Execute());
            Assert.Contains("AngularVelocity", exception.Message); // Проверяем, что ошибка связана с угловой скоростью
        }

        [Fact(DisplayName = "[004] Rotate an object whose angle cannot be changed results in an error")]
        public void Rotate_ThrowsWhenAngleCannotBeSet_Exception()
        {
            // Arrange
            var testObject = new TestRotateObjectWithSetAngleFailure();
            testObject.SetProperty("Angle", new Angle(45)); // Угол задан
            testObject.SetProperty("AngularVelocity", 10); // Угловая скорость задана

            // Запрещаем изменение угла
            testObject.DisallowSettingAngle();

            var adapter = new RotatableAdapter(testObject);
            var rotate = new Rotate(adapter);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => rotate.Execute());
            Assert.Contains("Angle cannot be set", exception.Message); // Проверяем сообщение об ошибке
        }
    }

    
}
