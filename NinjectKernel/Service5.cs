using System;

namespace NinjectKernel
{
    public class Service5 : IDisposable
    {
        private int _counter;
        private readonly IReader _reader;


        public Service5(IReader reader)
        {
            _reader = reader;
            _counter = 0;
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing Service 5");
        }

        public void Read()
        {
            Console.WriteLine(_counter++);
            Console.WriteLine(_reader.Read());
        }
    }
}