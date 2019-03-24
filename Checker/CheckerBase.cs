using System;
using System.Diagnostics;
using System.Reflection;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public abstract class CheckerBase
    {
        public string GetAssemblyVersionString()
        {
            var assembly = Assembly.GetAssembly(ContainerType);
            var name = assembly.GetName();
            return name.ToString();
        }

        public string GetAssemblyString()
        {
            var assembly = Assembly.GetAssembly(ContainerType);
            var name = assembly.GetName();
            return name.ToString().Split(",")[0];
        }

        public bool Run(Type t)
        {
            var container = CreateContainer(t);
            var finalized = false;
            InnerRun(container, t, () => {finalized = true;});

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return finalized;
        }

        protected abstract Type ContainerType {get;}
        private void InnerRun(object container, Type t, Action callback)
        {
            // Resolve in this scope.
            var obj = Resolve(container, t);
            obj.FinalizeCallback = callback;

            // Scope out, then no reference to obj.
        }

        protected abstract object CreateContainer(Type t);
        protected abstract FinalizeCallbackable Resolve(object container, Type t);
    }
}