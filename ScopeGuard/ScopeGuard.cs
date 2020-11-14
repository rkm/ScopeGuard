using System;


namespace ScopeGuard
{
    public class ScopeGuard : IDisposable
    {
        protected readonly Action Callback;

        public ScopeGuard(Action callback)
        {
            Callback = callback ?? throw new ArgumentException(nameof(callback));
        }

        // ^IDisposable
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        // ^IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Callback();
        }
    }
}
