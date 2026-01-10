using Game.Common.Pieces;

namespace Game.Gameplay.Events.Events
{
    public class SetBagNextPieceEvent : IEvent
    {
        public readonly PieceType PieceType;

        public SetBagNextPieceEvent(PieceType pieceType)
        {
            PieceType = pieceType;
        }
    }
}