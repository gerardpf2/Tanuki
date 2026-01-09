namespace Game.Gameplay.Phases.Phases
{
    public abstract class Phase : IPhase
    {
        private int _resolveTimesPerIteration;

        protected virtual int? MaxResolveTimesPerIteration => null;

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
            return !MaxResolveTimesPerIteration.HasValue || _resolveTimesPerIteration < MaxResolveTimesPerIteration.Value;
        }
    }
}