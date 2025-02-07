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
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                    Console.WriteLine("Внешний ресурс освобожден.");
                }

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
                throw;
            }
            finally
            {
                r.Dispose();
            }
        }
    }
}
