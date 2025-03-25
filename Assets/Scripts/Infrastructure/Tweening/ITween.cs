namespace Infrastructure.Tweening
{
    public interface ITween
    {
        TweenState State { get; }

        void Update(float deltaTimeS, bool backwards = false);

        bool Play();

        bool Pause();

        void Restart(bool withDelay);
    }
}