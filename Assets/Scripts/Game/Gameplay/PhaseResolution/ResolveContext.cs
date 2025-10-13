using Game.Gameplay.Board;

namespace Game.Gameplay.PhaseResolution
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