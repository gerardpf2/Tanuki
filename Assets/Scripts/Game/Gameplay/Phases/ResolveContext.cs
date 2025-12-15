using Game.Gameplay.Board;

namespace Game.Gameplay.Phases
{
    public class ResolveContext
    {
        public Coordinate? PieceSourceCoordinate { get; private set; }

        public Coordinate? PieceLockSourceCoordinate { get; private set; }

        public ResolveContext(Coordinate? pieceSourceCoordinate, Coordinate? pieceLockSourceCoordinate)
        {
            SetPieceSourceCoordinate(pieceSourceCoordinate, pieceLockSourceCoordinate);
        }

        public void SetPieceSourceCoordinate(Coordinate? pieceSourceCoordinate, Coordinate? pieceLockSourceCoordinate)
        {
            PieceSourceCoordinate = pieceSourceCoordinate;
            PieceLockSourceCoordinate = pieceLockSourceCoordinate;
        }
    }
}