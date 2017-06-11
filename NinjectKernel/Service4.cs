using System;
using Ninject;

namespace NinjectKernel
{
    public class Service4
    {
        [Inject, Named("service")]
        public IWriter Writer { get; set; }
        [Inject]
        public IReader Reader { get; set; }

        public void Read()
        {
            Console.WriteLine(Reader.Read());
        }

        public void Write()
        {
            Writer.Write("message from service 4");
        }
    }
}