using System;
using Autofac;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public class AutofacChecker : CheckerBase
    {
        protected override object CreateContainer<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<T>();
            return builder.Build();

        }
        protected override FinalizeCallbackable Resolve<T>(object container)
        {
            return ((IContainer)container).Resolve<T>();
        }
    }
}