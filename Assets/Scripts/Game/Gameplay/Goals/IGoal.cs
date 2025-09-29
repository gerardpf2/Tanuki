using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public interface IGoal
    {
        PieceType PieceType { get; }

        int Amount { get; }
    }
}