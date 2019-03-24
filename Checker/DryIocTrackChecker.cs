using System;
using TrackingCheck.Model;
using DryIoc;

namespace TrackingCheck.Checker
{
    public class DryIocTrackChecker : CheckerBase
    {
        protected override Type ContainerType => typeof(Container);

        public override string ContainerName => "DryIocTrack";

        protected override object CreateContainer(Type t)
        {
            var container = new Container();
            container.Register(t, setup: Setup.With(trackDisposableTransient: true));
            return container;
        }
        protected override FinalizeCallbackable Resolve(object container, Type t)
        {
            return (FinalizeCallbackable)
                ((Container)container).Resolve(t);
        }
    }
}