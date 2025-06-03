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

        public virtual void Uninitialize()
        {
            _resolveTimes = 0;
        }

        public virtual void OnBeginIteration() { }

        public ResolveResult Resolve(ResolveContext resolveContext)
        {
            if (!CanResolve())
            {
                return ResolveResult.NotUpdated;
            }

            ResolveResult resolveResult = ResolveImpl(resolveContext);

            if (resolveResult is ResolveResult.Updated)
            {
                ++_resolveTimes;
                ++_resolveTimesPerIteration;
            }

            return resolveResult;
        }

        protected abstract ResolveResult ResolveImpl(ResolveContext resolveContext);

        public virtual void OnEndIteration()
        {
            _resolveTimesPerIteration = 0;
        }

        private bool CanResolve()
        {
            return
                (_maxResolveTimes < 0 || _resolveTimes < _maxResolveTimes) &&
                (_maxResolveTimesPerIteration < 0 || _resolveTimesPerIteration < _maxResolveTimesPerIteration);
        }
    }
}