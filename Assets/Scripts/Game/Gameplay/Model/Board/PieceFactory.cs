using Game.Gameplay.Model.Board.Pieces;

namespace Game.Gameplay.Model.Board
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