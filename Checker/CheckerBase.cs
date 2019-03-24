using System;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public abstract class CheckerBase
    {
        private bool _finalized;
        
        public bool Run(Type t)
        {
            var container = CreateContainer(t);
            InnerRun(container, t);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return _finalized;
        }

        private void InnerRun(object container, Type t)
        {
            var obj = Resolve(container, t);
            _finalized = false;
            obj.FinalizeCallback = () => { _finalized = true; };
        }

        protected abstract object CreateContainer(Type t);
        protected abstract FinalizeCallbackable Resolve(object container, Type t);
    }
}