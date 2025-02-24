using System.Collections.Concurrent;

namespace SpaceshipOtus.Homework3
{
    public class CommandHandler
    {
        private readonly BlockingCollection<ICommand> _queue = new(100);

        public void StartProcessing()
        {
            foreach (ICommand command in _queue.GetConsumingEnumerable())
            {
                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    var handlerCommand = ExceptionHandler.Handle(command, ex);

                    if (handlerCommand is LogCommand)
                    {
                        _queue.Add(handlerCommand); //Добавляем команду LogCommand в очередь
                    }

                    handlerCommand.Execute();
                }
            }
        }

        public void AddCommand(ICommand command)
        {
            _queue.Add(command);
        }
    }
}
