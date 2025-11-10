namespace Game.Gameplay.Board
{
    public interface IBoardContainer
    {
        IBoard Board { get; }

        void Initialize(IBoard board);

        void Uninitialize();
    }
}