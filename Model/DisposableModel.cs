using System;

namespace TrackingCheck.Model
{
    public class DisposableModel : FinalizeCallbackable, IDisposable
    {
        public void Dispose()
        {
            // Do nothing.
        }
    }
}