namespace Game.Gameplay.Board
{
    public interface IBoardController
    {
        int Rows { get; }

        int Columns { get; }

        void Initialize(string boardId);

        void ResolveInstantiateInitialAndCascade();
    }
}