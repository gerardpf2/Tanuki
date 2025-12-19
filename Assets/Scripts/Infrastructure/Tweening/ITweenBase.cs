namespace Infrastructure.Tweening
{
    public interface ITweenBase
    {
        TweenState State { get; }

        bool Paused { get; }

        // Returns remaining deltaTimeS
        float Step(float deltaTimeS, bool backwards = false);

        void Pause();

        void Resume();

        void Restart();

        // Returns remaining deltaTimeS
        public float Update(float deltaTimeS, bool backwards = false)
        {
            while (deltaTimeS > 0.0f && State is not TweenState.Complete)
            {
                deltaTimeS = Step(deltaTimeS, backwards);
            }

            return deltaTimeS;
        }
    }
}