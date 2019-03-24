using System;
using TrackingCheck.Model;
using Grace.DependencyInjection;

namespace TrackingCheck.Checker
{
    public class GraceChecker : CheckerBase
    {
        protected override Type ContainerType => typeof(DependencyInjectionContainer);

        protected override object CreateContainer(Type t)
        {
            var container = new DependencyInjectionContainer();
            container.Configure(c => c.Export(t));
            return container;
        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((DependencyInjectionContainer)container).Locate(t);
        }
    }
}