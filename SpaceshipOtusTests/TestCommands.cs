using SpaceshipOtus.Homework3;

namespace SpaceshipOtusTests
{
    public class TestForLogCommand : ICommand
    {
        public void Execute()
        {
            throw new InvalidOperationException("Test exception");
        }
    }

    public class TestForRetryCommand : ICommand
    {
        public int ExecutionCount { get; private set; } = 0;

        public void Execute()
        {
            ExecutionCount++;

            if (ExecutionCount == 1)
            {
                throw new InvalidOperationException("Test exception");
            }
        }
    }

    public class TestForRetryTwiceThenLogCommand : ICommand
    {
        public int ExecutionCount { get; private set; } = 0;

        public void Execute()
        {
            ExecutionCount++;
            throw new InvalidOperationException("Test exception");
        }
    }
}
