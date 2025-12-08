using Game.Gameplay.Board;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class InstantiatePlayerPieceEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;
        public readonly Coordinate SourceCoordinate;

        public InstantiatePlayerPieceEvent([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Piece = piece;
            SourceCoordinate = sourceCoordinate;
        }
    }
}