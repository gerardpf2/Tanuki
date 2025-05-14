namespace Game.Gameplay.PhaseResolution.Phases
{
    public abstract class Phase : IPhase
    {
        private readonly int _maxResolveTimes;
        private readonly int _maxResolveTimesPerIteration;

        private int _resolveTimes;
        private int _resolveTimesPerIteration;

        protected Phase(int maxResolveTimes, int maxResolveTimesPerIteration)
        {
            _maxResolveTimes = maxResolveTimes;
            _maxResolveTimesPerIteration = maxResolveTimesPerIteration;
        }

        public virtual void OnBeginIteration()
        {
            _resolveTimesPerIteration = 0;
        }

        public bool Resolve()
        {
            bool resolved = CanResolve() && ResolveImpl();

            if (resolved)
            {
                ++_resolveTimes;
                ++_resolveTimesPerIteration;
            }

            return resolved;
        }

        protected abstract bool ResolveImpl();

        public virtual void OnEndIteration() { }

        private bool CanResolve()
        {
            return
                (_maxResolveTimes < 0 || _resolveTimes < _maxResolveTimes) &&
                (_maxResolveTimesPerIteration < 0 || _resolveTimesPerIteration < _maxResolveTimesPerIteration);
        }
    }
}