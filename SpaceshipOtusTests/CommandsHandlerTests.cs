using Microsoft.AspNetCore.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SpaceshipOtus.Homework2;
using SpaceshipOtus.Homework3;
using System.Collections.Concurrent;

namespace SpaceshipOtusTests
{
    public class CommandsHandlerTests
    {
        [Fact(DisplayName = "[001] Check LogCommand")]
        public void LogCommand_ShouldLogException()
        {
            // Arrange
            var testForLogCommand = new TestForLogCommand();
            var exceptionMessage = "Test exception";
            var exception = new InvalidOperationException(exceptionMessage);
            var logCommand = new LogCommand(testForLogCommand, exception);

            // Act
            logCommand.Execute();

            // Assert
            Assert.Equal(exceptionMessage, logCommand.Message);
        }

        [Fact(DisplayName = "[002] Check LogCommand To Queue")]
        public void ExceptionHandler_ShouldAddLogCommandToQueue()
        {
            // Arrange
            var queue = new BlockingCollection<ICommand>();
            var command = new TestForLogCommand();
            var exception = new InvalidOperationException("Test exception");

            // Регистрируем обработчик для TestCommand и InvalidOperationException
            ExceptionHandler.Register(typeof(TestForLogCommand), typeof(InvalidOperationException), ExceptionHandler.LogToQueueHandler);

            // Act
            var handlerCommand = ExceptionHandler.Handle(command, exception);
            queue.Add(handlerCommand);
            var logCommand = queue.Take();

            // Assert
            Assert.IsType<LogCommand>(logCommand); // Проверяем, что это LogCommand
        }

        [Fact(DisplayName = "[003] Check RetryCommand")]
        public void RetryCommand_ShouldRetryOriginalCommand()
        {
            // Arrange
            var testForRetryCommand = new TestForRetryCommand();
            var retryCommand = new RetryCommand(testForRetryCommand);

            // Act
            retryCommand.Execute();

            // Assert
            Assert.Equal(2, testForRetryCommand.ExecutionCount); // Проверяем, что команда выполнилась дважды
        }

        [Fact(DisplayName = "[004] Check RetryCommand To Queue")]
        public void ExceptionHandler_ShouldAddRetryCommandToQueue()
        {
            // Arrange
            var queue = new BlockingCollection<ICommand>();
            var command = new TestForRetryCommand();
            var exception = new InvalidOperationException("Test exception");

            // Регистрируем обработчик для TestCommand и InvalidOperationException
            ExceptionHandler.Register(typeof(TestForRetryCommand), typeof(InvalidOperationException), ExceptionHandler.RetryCommandHandler);

            // Act
            var handlerCommand = ExceptionHandler.Handle(command, exception);
            queue.Add(handlerCommand);

            // Assert
            Assert.Single(queue); // Проверяем, что в очереди одна команда
            var retryCommand = queue.Take();
            Assert.IsType<RetryCommand>(retryCommand); // Проверяем, что это RetryCommand
        }
    }
}
