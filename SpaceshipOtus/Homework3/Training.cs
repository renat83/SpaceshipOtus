using System.Collections.Concurrent;

namespace SpaceshipOtus.Homework3
{
    public interface IBase
    {
        void Function(Resource resource);
    }

    public class Concret : IBase
    {
        public Concret() { }

        public void Function(Resource resource)
        {
            throw new NotImplementedException();
        }
    }

    public class Resource : IDisposable
    {
        private bool _disposed = false;

        public Resource()
        {
            Console.WriteLine("Внешний ресурс подключен.");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                // Освобождаем управляемые ресурсы
                Console.WriteLine("Внешний ресурс освобожден.");
                // Освобождаем неуправляемые ресурсы (если есть)
                _disposed = true;
            }
        }
    }

    public class Training
    {
        IBase b = new Concret();

        public void MethodForTryCatchWork(IBase b)
        {
            var r = new Resource();
            try
            {
                b.Function(r);
            }
            catch (Exception)
            {
                //handler
                throw;
            }
            finally
            {
                r.Dispose();
            }
        }

        public void Method(IBase b)
        {
            using (Resource r1 = new Resource())
            {
                b.Function(r1);
            }
        }
    }

    public interface IComand
    {
        void Execute();
    }

    public class Temp
    {
        BlockingCollection<IComand> q = new BlockingCollection<IComand>();

        public void MyFunc() 
        {
            var stop = false;

            while (!stop)
            {
                IComand cmd = q.Take();

                try
                {
                    cmd.Execute();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(cmd, ex).Execute();
                }
                
            }
        }
    }

    public class ExceptionHandler
    {
        public ExceptionHandler() 
        { 
        }

        public static IComand Handle(IComand cmd, Exception ex)
        {

        }
    }

}
