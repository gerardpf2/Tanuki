namespace Game.Gameplay.View.EventResolution
{
    public interface IEventListener
    {
        bool Resolving { get; }

        void Initialize();

        void Uninitialize();
    }
}