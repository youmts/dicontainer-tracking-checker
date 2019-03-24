using System;

namespace TrackingCheck.Model
{
    public class Disposable : FinalizeCallbackable, IDisposable
    {
        public void Dispose()
        {
            // Do nothing.
        }
    }
}