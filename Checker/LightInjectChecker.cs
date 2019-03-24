using System;
using LightInject;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public class LightInjectChecker : CheckerBase
    {
        protected override object CreateContainer(Type t)
        {
            var container = new ServiceContainer();
            container.Register(t);
            container.Compile();
            return container;

        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((IServiceContainer)container).GetInstance(t);
        }
    }
}