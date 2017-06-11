using System;
using Ninject;
using Ninject.Modules;

namespace NinjectKernel
{
    class Module : NinjectModule
    {       
        public override void Load()
        {
            Bind<IWriter>().To<ServiceWriter>();
            Bind<IReader>().To<DBReader>();
        }
    }
}
