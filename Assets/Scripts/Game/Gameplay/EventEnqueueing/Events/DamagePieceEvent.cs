using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DamagePieceEvent : IEvent
    {
        public readonly IPiece Piece;

        public DamagePieceEvent(IPiece piece)
        {
            Piece = piece;
        }
    }
}