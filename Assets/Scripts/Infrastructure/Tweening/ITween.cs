namespace Infrastructure.Tweening
{
    public interface ITween
    {
        TweenState State { get; }

        void Update(float deltaTimeS, bool backwards = false);

        bool Pause();

        bool Resume();

        void Restart(bool withDelay);
    }
}