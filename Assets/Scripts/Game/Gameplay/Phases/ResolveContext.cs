using Game.Gameplay.Board;

namespace Game.Gameplay.Phases
{
    public class ResolveContext
    {
        public readonly Coordinate? PieceSourceCoordinate;

        public ResolveContext(Coordinate? pieceSourceCoordinate)
        {
            PieceSourceCoordinate = pieceSourceCoordinate;
        }
    }
}