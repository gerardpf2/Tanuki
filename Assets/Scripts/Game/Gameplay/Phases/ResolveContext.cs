using Game.Gameplay.Board;

namespace Game.Gameplay.Phases
{
    public class ResolveContext
    {
        public readonly ResolveReason ResolveReason;

        public Coordinate? PieceSourceCoordinate { get; private set; }

        public Coordinate? PieceLockSourceCoordinate { get; private set; }

        public ResolveContext(
            ResolveReason resolveReason,
            Coordinate? pieceSourceCoordinate,
            Coordinate? pieceLockSourceCoordinate)
        {
            ResolveReason = resolveReason;

            SetPieceSourceCoordinate(pieceSourceCoordinate, pieceLockSourceCoordinate);
        }

        public void SetPieceSourceCoordinate(Coordinate? pieceSourceCoordinate, Coordinate? pieceLockSourceCoordinate)
        {
            PieceSourceCoordinate = pieceSourceCoordinate;
            PieceLockSourceCoordinate = pieceLockSourceCoordinate;
        }
    }
}