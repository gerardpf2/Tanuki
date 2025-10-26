namespace Game.Gameplay.View.EventResolvers
{
    public interface IEventsResolver
    {
        bool Resolving { get; }

        void Initialize();

        void Uninitialize();
    }
}