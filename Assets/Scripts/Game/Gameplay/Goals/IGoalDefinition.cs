using Game.Gameplay.Board;

namespace Game.Gameplay.Goals
{
    public interface IGoalDefinition
    {
        public PieceType PieceType { get; }

        public int Amount { get; }
    }
}