using System;
using System.Diagnostics;

namespace TrackingCheck.Model
{
    public abstract class FinalizeCallbackable
    {
        public Action FinalizeCallback {get; set;} = null;

        ~FinalizeCallbackable()
        {
            Debug.WriteLine($"Finalize({this.GetType()})");
            FinalizeCallback?.Invoke();
        }
    }
}