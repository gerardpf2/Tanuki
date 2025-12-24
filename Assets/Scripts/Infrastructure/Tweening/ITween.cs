namespace Infrastructure.Tweening
{
    public interface ITween : ITweenBase
    {
        object Target { get; }

        float DurationS { get; }
    }

    public interface ITween<out TTarget, out T> : ITween
    {
        new TTarget Target { get; }

        T Start { get; }

        T End { get; }
    }
}