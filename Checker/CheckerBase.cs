using System;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public abstract class CheckerBase
    {
        private bool _finalized;
        
        public bool Run<T>() where T : FinalizeCallbackable
        {
            var container = CreateContainer<T>();
            InnerRun<T>(container);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return _finalized;
        }

        private void InnerRun<T>(object container) where T : FinalizeCallbackable
        {
            var obj = Resolve<T>(container);
            _finalized = false;
            obj.FinalizeCallback = () => { _finalized = true; };
        }

        protected abstract object CreateContainer<T>() 
            where T : FinalizeCallbackable;
        protected abstract FinalizeCallbackable Resolve<T>(object container)
            where T : FinalizeCallbackable;
    }
}