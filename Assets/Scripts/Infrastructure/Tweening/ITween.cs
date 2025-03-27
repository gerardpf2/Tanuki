namespace Infrastructure.Tweening
{
    public interface ITween
    {
        TweenState State { get; }

        // Returns remaining deltaTimeS
        float Update(float deltaTimeS, bool backwards = false);

        bool Pause();

        bool Resume();

        void Restart();
    }
}