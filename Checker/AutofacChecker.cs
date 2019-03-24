using System;
using Autofac;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public class AutofacChecker : CheckerBase
    {
        protected override Type ContainerType => typeof(IContainer);

        protected override object CreateContainer(Type t)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(t);
            return builder.Build();

        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((IContainer)container).Resolve(t);
        }
    }
}