namespace Game.Gameplay.Moves
{
    public interface IMovesContainer
    {
        IMoves Moves { get; }

        void Initialize(IMoves moves);

        void Uninitialize();
    }
}