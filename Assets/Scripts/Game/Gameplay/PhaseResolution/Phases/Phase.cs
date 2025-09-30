namespace Game.Gameplay.PhaseResolution.Phases
{
    public abstract class Phase : IPhase
    {
        private int _resolveTimes;
        private int _resolveTimesPerIteration;

        protected virtual int? MaxResolveTimes => null;

        protected virtual int? MaxResolveTimesPerIteration => null;

        public virtual void Initialize()
        {
            Uninitialize();
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
                (!MaxResolveTimes.HasValue || _resolveTimes < MaxResolveTimes.Value) &&
                (!MaxResolveTimesPerIteration.HasValue || _resolveTimesPerIteration < MaxResolveTimesPerIteration.Value);
        }
    }
}