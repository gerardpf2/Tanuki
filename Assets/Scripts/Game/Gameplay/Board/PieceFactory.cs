using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public class PieceFactory : IPieceFactory
    {
        public IPiece GetTest()
        {
            // TODO

            return new Test(true, 0);
        }
    }
}