using Game.Gameplay.Model.Board.Pieces;

namespace Game.Gameplay.Model.Board
{
    public class PieceFactory : IPieceFactory
    {
        public IPiece GetTest()
        {
            return new Test();
        }
    }
}