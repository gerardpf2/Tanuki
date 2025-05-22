using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public class PieceSerializedDataConverter : IPieceSerializedDataConverter
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;

        public PieceSerializedDataConverter([NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _pieceGetter = pieceGetter;
        }

        public IPiece To([NotNull] PieceSerializedData pieceSerializedData)
        {
            ArgumentNullException.ThrowIfNull(pieceSerializedData);

            return _pieceGetter.Get(pieceSerializedData.PieceType, pieceSerializedData.CustomData);
        }

        public PieceSerializedData From([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return
                new PieceSerializedData
                {
                    PieceType = piece.Type,
                    CustomData = new Dictionary<string, object>(piece.CustomData)
                };
        }
    }
}