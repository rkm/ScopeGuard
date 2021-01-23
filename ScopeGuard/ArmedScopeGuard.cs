using System;


namespace ScopeGuard
{
    public class ArmedScopeGuard : ScopeGuard
    {
        private bool _armed = true;

        public ArmedScopeGuard(Action callback)
            : base(callback)
        {
        }

        public void Disarm()
        {
            _armed = false;
        }

        // ^IDisposable
        protected override void Dispose(bool disposing)
        {
            if (disposing && _armed)
                Callback();
        }
    }
}
