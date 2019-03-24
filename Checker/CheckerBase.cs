using System;
using System.Diagnostics;
using System.Reflection;
using TrackingCheck.Model;

namespace TrackingCheck.Checker
{
    public abstract class CheckerBase
    {
        public string GetAssemblyString()
        {
            var assembly = Assembly.GetAssembly(ContainerType);
            var name = assembly.GetName();
//            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return name.ToString();
        }
        
        public bool Run(Type t)
        {
            var container = CreateContainer(t);
            InnerRun(container, t);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return _finalized;
        }

        protected abstract Type ContainerType {get;}
        private bool _finalized;
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