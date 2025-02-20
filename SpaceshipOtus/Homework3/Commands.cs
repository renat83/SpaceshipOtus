namespace SpaceshipOtus.Homework3
{
    public interface ICommand
    {
        void Execute();
    }

    /// <summary>
    /// Команда, которая записывает информацию о выброшенном исключении в лог.
    /// </summary>
    public class LogCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly Exception _exception;
        public string Message { get; set; } = string.Empty;

        public LogCommand(ICommand command, Exception exception)
        {
            _command = command;
            _exception = exception;
        }

        public void Execute()
        {
            Message = _exception.Message;
            // Логирование информации о команде и исключении
            Console.WriteLine($"Logging exception: {Message} from command: {_command.GetType().Name}");
        }
    }

    /// <summary>
    /// Команда, которая повторяет Команду, выбросившую исключение.
    /// </summary>
    public class RetryCommand : ICommand
    {
        private readonly ICommand _command;
        private int _retryCount;

        public RetryCommand(ICommand command)
        {
            _command = command;
        }

        public void Execute()
        {
            try
            {
                _command.Execute();
            }
            catch (Exception ex)
            {
                _retryCount++;

                if (_retryCount < 2)
                {
                    Console.WriteLine($"{ex.Message}. Retrying command");
                    _command.Execute();
                }
                else
                {
                    // Логируем исключение при повторной ошибке
                    var logCommand = new LogCommand(_command, ex);
                    logCommand.Execute();
                }
            }
        }
    }
}
