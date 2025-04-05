using SpaceshipOtus.Homework3;
using SpaceshipOtus.Homework4;
using SpaceshipOtus.Homework5;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpaceshipOtusTests
{
    public class IoCTests
    {
        public interface ISpaceshipCommand : ICommand { }
        public class MoveCommand : ISpaceshipCommand { public void Execute() => Console.WriteLine("Moving"); }
        public class RotateCommand : ISpaceshipCommand { public void Execute() => Console.WriteLine("Rotating"); }
        public class FireCommand : ISpaceshipCommand { public void Execute() => Console.WriteLine("Firing"); }
        public class LimitedMoveCommand : ISpaceshipCommand { public void Execute() => Console.WriteLine("Limited Moving"); }

        [Fact(DisplayName = "[001] Register And Resolve Command")]
        public void Register_And_Resolve_Command()
        {
            // Arrange
            var registerCmd = IoC.Resolve<CommandAbstract>(
                "IoC.Register",
                "Move",
                "root",
                new Func<object[], object>(_ => new MoveCommand()));

            // Act
            registerCmd.Execute();
            var command = IoC.Resolve<ISpaceshipCommand>("Move");

            // Assert
            Assert.IsType<MoveCommand>(command);
        }

        [Fact(DisplayName = "[002] Resolve Throws When Dependency Not Found")]
        public void Resolve_Throws_When_Dependency_Not_Found()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                IoC.Resolve<object>("NonExistentCommand"));
        }

        [Fact(DisplayName = "[003] Dependency Resolution Falls Back To Root Scope")]
        public void Dependency_Resolution_Falls_Back_To_Root_Scope()
        {
            // Arrange
            IoC.Resolve<CommandAbstract>("IoC.Register", "Fire", "root",
                new Func<object[], object>(_ => new FireCommand())).Execute();

            IoC.Resolve<CommandAbstract>("Scopes.New", "game1").Execute();
            IoC.Resolve<CommandAbstract>("Scopes.Current", "game1").Execute();

            // Act
            var command = IoC.Resolve<ISpaceshipCommand>("Fire");

            // Assert
            Assert.IsType<FireCommand>(command);
        }

        [Fact(DisplayName = "[004] Override Dependencies In Child Scope")]
        public void Override_Dependencies_In_Child_Scope()
        {
            // Arrange
            IoC.Resolve<CommandAbstract>("IoC.Register", "Move", "root",
                new Func<object[], object>(_ => new MoveCommand())).Execute();

            IoC.Resolve<CommandAbstract>("Scopes.New", "limited").Execute();
            IoC.Resolve<CommandAbstract>("Scopes.Current", "limited").Execute();

            IoC.Resolve<CommandAbstract>("IoC.Register", "Move", "limited",
                new Func<object[], object>(_ => new LimitedMoveCommand())).Execute();

            // Act
            var limitedCommand = IoC.Resolve<ISpaceshipCommand>("Move");
            IoC.Resolve<CommandAbstract>("Scopes.Current", "root").Execute();
            var rootCommand = IoC.Resolve<ISpaceshipCommand>("Move");

            // Assert
            Assert.IsType<LimitedMoveCommand>(limitedCommand);
            Assert.IsType<MoveCommand>(rootCommand);
        }

        [Fact(DisplayName = "[005] ThreadSafety Test")]
        public void ThreadSafety_Test()
        {
            // Arrange
            const int threadCount = 10;
            var countdown = new CountdownEvent(threadCount);

            // Act
            for (int i = 0; i < threadCount; i++)
            {
                new Thread(() =>
                {
                    try
                    {
                        var scopeId = $"game_{Thread.CurrentThread.ManagedThreadId}";
                        IoC.Resolve<CommandAbstract>("Scopes.New", scopeId).Execute();
                        IoC.Resolve<CommandAbstract>("Scopes.Current", scopeId).Execute();
                        IoC.Resolve<CommandAbstract>("IoC.Register", "ThreadSpecific", scopeId,
                            new Func<object[], object>(_ => Thread.CurrentThread.ManagedThreadId)).Execute();

                        var value = IoC.Resolve<int>("ThreadSpecific");
                        Assert.Equal(Thread.CurrentThread.ManagedThreadId, value);
                    }
                    finally
                    {
                        countdown.Signal();
                    }
                }).Start();
            }

            // Assert
            Assert.True(countdown.Wait(TimeSpan.FromSeconds(5)), "Not all threads completed in time");
        }

        [Fact(DisplayName = "[006] Parallel Operations Test")]
        public async Task Parallel_Operations_Test()
        {
            // Arrange
            var tasks = new Task[5];

            // Act
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    var scopeId = $"task_{Task.CurrentId}";
                    IoC.Resolve<CommandAbstract>("Scopes.New", scopeId).Execute();
                    IoC.Resolve<CommandAbstract>("Scopes.Current", scopeId).Execute();
                    IoC.Resolve<CommandAbstract>("IoC.Register", "TaskData", scopeId,
                        new Func<object[], object>(_ => $"Data from task {Task.CurrentId}")).Execute();

                    var data = IoC.Resolve<string>("TaskData");
                    Assert.Contains($"Data from task {Task.CurrentId}", data);
                });
            }

            // Assert
            await Task.WhenAll(tasks);
        }

        [Fact(DisplayName = "[007] Scope Isolation Test")]
        public void Scope_Isolation_Test()
        {
            // Arrange
            IoC.Resolve<CommandAbstract>("Scopes.New", "scope1").Execute();
            IoC.Resolve<CommandAbstract>("Scopes.New", "scope2").Execute();

            // Act - Scope 1
            IoC.Resolve<CommandAbstract>("Scopes.Current", "scope1").Execute();
            IoC.Resolve<CommandAbstract>("IoC.Register", "Data", "scope1",
                new Func<object[], object>(_ => "Scope1 Data")).Execute();

            // Act - Scope 2
            IoC.Resolve<CommandAbstract>("Scopes.Current", "scope2").Execute();
            IoC.Resolve<CommandAbstract>("IoC.Register", "Data", "scope2",
                new Func<object[], object>(_ => "Scope2 Data")).Execute();

            // Assert
            IoC.Resolve<CommandAbstract>("Scopes.Current", "scope1").Execute();
            Assert.Equal("Scope1 Data", IoC.Resolve<string>("Data"));

            IoC.Resolve<CommandAbstract>("Scopes.Current", "scope2").Execute();
            Assert.Equal("Scope2 Data", IoC.Resolve<string>("Data"));
        }
    }
}