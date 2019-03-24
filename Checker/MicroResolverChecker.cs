using System;
using TrackingCheck.Model;
using MicroResolver;

namespace TrackingCheck.Checker
{
    public class MicroResolverChecker : CheckerBase
    {
        protected override Type ContainerType => typeof(ObjectResolver);

        protected override object CreateContainer(Type t)
        {
            var container = ObjectResolver.Create();
            container.Register(Lifestyle.Transient, t, t);
            container.Compile();
            return container;
        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((ObjectResolver)container).Resolve(t);
        }
    }
}