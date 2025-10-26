using Game.Gameplay.Pieces;

namespace Game.Gameplay.Bag
{
    public class BagPieceEntry
    {
        public readonly PieceType PieceType;
        public readonly int Amount;

        public BagPieceEntry(PieceType pieceType, int amount)
        {
            PieceType = pieceType;
            Amount = amount;
        }
    }
}