namespace Game.Gameplay.View.EventResolution
{
    public interface IEventsResolver
    {
        bool Resolving { get; }

        void Initialize();

        void Uninitialize();
    }
}