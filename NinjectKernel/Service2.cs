using System;
using Ninject;

namespace NinjectKernel
{
    public class Service2
    {
        private IReader _reader;
        private IWriter _writer;
        //[Inject]
        public void Read(IReader read)
        {
            _reader = read;
            Console.WriteLine("Prepare reder");
        }
        //[Inject]
        public void Write(IWriter write)
        {
            _writer = write;
            Console.WriteLine("Prepare writer");
        }

        public void TryRead()
        {
            Console.WriteLine(_reader.Read());
        }

        public void TryWrite()
        {
            _writer.Write("message from service 2");
        }
    }
}