namespace Infrastructure.Tweening
{
    public interface ITween
    {
        TweenState Update(float deltaTimeS);

        bool Pause();

        bool Resume();
    }
}