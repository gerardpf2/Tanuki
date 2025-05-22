using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public class PieceFactory : IPieceFactory
    {
        public IPiece GetTest(IEnumerable<KeyValuePair<string, object>> customData)
        {
            return ProcessCustomData(new Test(), customData);
        }

        public IPiece GetPlayerBlock11()
        {
            return new PlayerBlock11();
        }

        public IPiece GetPlayerBlock12()
        {
            return new PlayerBlock12();
        }

        public IPiece GetPlayerBlock21()
        {
            return new PlayerBlock21();
        }

        private static IPiece ProcessCustomData(IPiece piece, IEnumerable<KeyValuePair<string, object>> customData)
        {
            if (piece is IPieceUpdater pieceUpdater)
            {
                pieceUpdater.ProcessCustomData(customData);
            }

            return piece;
        }
    }
}