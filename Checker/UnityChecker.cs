using System;
using TrackingCheck.Model;
using Unity;

namespace TrackingCheck.Checker
{
    public class UnityChecker : CheckerBase
    {
        protected override Type ContainerType => typeof(UnityContainer);

        protected override object CreateContainer(Type t)
        {
            var container = new UnityContainer();
            container.RegisterType(t);
            return container;
        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((UnityContainer)container).Resolve(t);
        }
    }
}