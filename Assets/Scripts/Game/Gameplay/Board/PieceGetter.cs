using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.Board
{
    public class PieceGetter : IPieceGetter
    {
        [NotNull] private readonly IPieceFactory _pieceFactory;

        public PieceGetter([NotNull] IPieceFactory pieceFactory)
        {
            ArgumentNullException.ThrowIfNull(pieceFactory);

            _pieceFactory = pieceFactory;
        }

        public IPiece Get(PieceType pieceType)
        {
            switch (pieceType)
            {
                case PieceType.Test:
                    return _pieceFactory.GetTest();
                default:
                    ArgumentOutOfRangeException.Throw(pieceType);
                    return null;
            }
        }
    }
}