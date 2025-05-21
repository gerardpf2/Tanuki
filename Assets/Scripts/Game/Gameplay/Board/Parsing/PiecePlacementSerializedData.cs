namespace Game.Gameplay.Board.Parsing
{
    public class PiecePlacementSerializedData
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public PieceSerializedData PieceSerializedData { get; set; }
    }
}