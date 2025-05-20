using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.Board
{
    public class PieceFactory : IPieceFactory
    {
        public IPiece GetTest()
        {
            // TODO: Add support for custom initial params

            return new Test(true, 0);
        }

        public IPiece GetPlayerBlock11()
        {
            return new PlayerBlock11();
        }
    }
}