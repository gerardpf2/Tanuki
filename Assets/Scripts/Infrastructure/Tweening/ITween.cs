namespace Infrastructure.Tweening
{
    public interface ITween : ITweenBase
    {
        // TODO: object Target { get; }
    }

    public interface ITween<TTarget, T> : ITween
    {
        // TODO: new TTarget Target { get; }
    }
}