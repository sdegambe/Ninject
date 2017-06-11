using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.NamedScope;

namespace NinjectKernel
{
    class Program
    {
        static void Main(string[] args)
        {
            //FromModule();
            //ConfigureByMethod();
            //WithMethod();
            //WithProperties();
            GetFromAssembly();

            Console.ReadLine();
        }

        private static void FromModule()
        {
            var kernel = new StandardKernel(new Module());           
            var service = kernel.Get<Service>();
            service.Read();
            service.Write();         
        }

        private static void ConfigureByMethod()
        {
            //two type binding
            var kernel = new StandardKernel();
            kernel.Bind<IReader>().To<DBReader>();
            kernel.Bind<IWriter>().To<ServiceWriter>();            
            var service = kernel.Get<Service2>();
            service.Write(kernel.Get<IWriter>());
            service.Read(kernel.Get<IReader>());
            service.TryRead();
            service.TryWrite();

            kernel.Rebind<IReader>().To<FileReader>();
            service = kernel.Get<Service2>();
            service.TryRead();
            service.TryWrite();
        }

        private static void WithMethod()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IReader>().To<DBReader>();
            kernel.Bind<IWriter>().To<ServiceWriter>();
            //inject without this
            kernel.Bind<Service2>()
                .ToMethod(c =>
                {
                    Console.WriteLine("Special method");
                    var result = new Service2();
                    var read = new DBReader();
                    var write = new FileWriter();
                    result.Read(read);
                    result.Write(write);
                    return result;

                });
            var service = kernel.Get<Service2>();
            service.TryWrite();
            service.TryRead();
        }

        private static void WithProperties()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IReader>().To<FileReader>();
            kernel.Bind<IWriter>().To<ServiceWriter>();
            kernel.Bind<Service3>().ToSelf().WithPropertyValue("Writer", e => kernel.Get<IWriter>())
                .WithPropertyValue("Reader", e => kernel.Get<IReader>());

            var service = kernel.Get<Service3>();
            service.Read();
            service.Write();
        }

        private static void GetFromAssembly()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IWriter>().To<ServiceWriter>().Named("service");
            kernel.Bind<IWriter>().To<FileWriter>().Named("service");
            //kernel.Bind<IWriter>().To<FileWriter>().Named("test");
            kernel.Bind<IReader>().To<FileReader>();
            var service = kernel.Get<Service4>();
            service.Write();
            service.Read();
        }        
    }
}
