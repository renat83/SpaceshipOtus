namespace SpaceshipOtus.Homework3
{
    public static class ExceptionHandler
    {
        private static readonly IDictionary<Type, IDictionary<Type, Func<ICommand, Exception, ICommand>>> _store =
            new Dictionary<Type, IDictionary<Type, Func<ICommand, Exception, ICommand>>>();

        public static void Register(Type commandType, Type exceptionType, Func<ICommand, Exception, ICommand> handler)
        {
            if (!_store.TryGetValue(commandType, out IDictionary<Type, Func<ICommand, Exception, ICommand>>? value))
            {
                value = new Dictionary<Type, Func<ICommand, Exception, ICommand>>();
                _store[commandType] = value;
            }

            value[exceptionType] = handler;
        }

        public static ICommand LogToQueueHandler(ICommand command, Exception exception)
        {
            return new LogCommand(command, exception);
        }

        public static ICommand RetryCommandHandler(ICommand command, Exception exception)
        {
            return new RetryCommand(command);
        }

        public static ICommand Handle(ICommand command, Exception exception)
        {
            Type commandType = command.GetType();
            Type exceptionType = exception.GetType();

            if (_store.TryGetValue(commandType, out IDictionary<Type, Func<ICommand, Exception, ICommand>>? exceptionHandlers))
            {
                if (exceptionHandlers.TryGetValue(exceptionType, out Func<ICommand, Exception, ICommand>? value))
                {
                    return value(command, exception);
                }
            }

            // Обработка по умолчанию
            return new LogCommand(command, exception);
        }


    }
}
