using System;
using TrackingCheck.Model;
using Abioc.Registration;
using Abioc;

namespace TrackingCheck.Checker
{
    public class AbiocChecker : CheckerBase
    {
        protected override Type ContainerType => typeof(AbiocContainer);

        protected override object CreateContainer(Type t)
        {
            var setup = new RegistrationSetup();
            setup.Register(t);
            return setup.Construct(GetType().Assembly);
        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((AbiocContainer)container).GetService(t);
        }
    }
}