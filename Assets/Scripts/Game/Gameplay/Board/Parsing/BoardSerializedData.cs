using System.Collections.Generic;

namespace Game.Gameplay.Board.Parsing
{
    public class BoardSerializedData
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        public List<PiecePlacementSerializedData> PiecePlacementSerializedData { get; set; }
    }
}