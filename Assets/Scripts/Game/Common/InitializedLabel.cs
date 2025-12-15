using Infrastructure.System.Exceptions;

namespace Game.Common
{
    public struct InitializedLabel
    {
        private bool _initialized;

        public void SetInitialized()
        {
            if (_initialized)
            {
                InvalidOperationException.Throw("Cannot initialize. Already initialized");
            }

            _initialized = true;
        }

        public void SetUninitialized()
        {
            if (!_initialized)
            {
                InvalidOperationException.Throw("Cannot uninitialize. Not initialized yet");
            }

            _initialized = false;
        }
    }
}