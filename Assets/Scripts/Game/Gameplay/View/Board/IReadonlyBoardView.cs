using Game.Gameplay.Board;

namespace Game.Gameplay.View.Board
{
    public interface IReadonlyBoardView
    {
        IReadonlyBoard Board { get; }
    }
}