using Game.Gameplay.Board;

namespace Game.Gameplay.Phases
{
    public class ResolveContext
    {
        public readonly Coordinate? PieceSourceCoordinate;
        public readonly Coordinate? PieceLockSourceCoordinate;

        public ResolveContext(Coordinate? pieceSourceCoordinate, Coordinate? pieceLockSourceCoordinate)
        {
            PieceSourceCoordinate = pieceSourceCoordinate;
            PieceLockSourceCoordinate = pieceLockSourceCoordinate;
        }
    }
}