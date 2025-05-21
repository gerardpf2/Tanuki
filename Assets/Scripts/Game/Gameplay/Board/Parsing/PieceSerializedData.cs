using System.Collections.Generic;

namespace Game.Gameplay.Board.Parsing
{
    public class PieceSerializedData
    {
        public PieceType PieceType { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }
}