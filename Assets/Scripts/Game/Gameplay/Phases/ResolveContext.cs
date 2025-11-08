using Game.Gameplay.Board;

namespace Game.Gameplay.Phases
{
    public class ResolveContext
    {
        public readonly bool ComesFromLock;

        public Coordinate? PieceSourceCoordinate { get; private set; }

        public Coordinate? PieceLockSourceCoordinate { get; private set; }

        public ResolveContext(
            bool comesFromLock,
            Coordinate? pieceSourceCoordinate,
            Coordinate? pieceLockSourceCoordinate)
        {
            ComesFromLock = comesFromLock;

            SetPieceSourceCoordinate(pieceSourceCoordinate, pieceLockSourceCoordinate);
        }

        public void SetPieceSourceCoordinate(Coordinate? pieceSourceCoordinate, Coordinate? pieceLockSourceCoordinate)
        {
            PieceSourceCoordinate = pieceSourceCoordinate;
            PieceLockSourceCoordinate = pieceLockSourceCoordinate;
        }
    }
}