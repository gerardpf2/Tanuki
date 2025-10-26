using System.Collections.Generic;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Parsing
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

            return _pieceGetter.Get(pieceSerializedData.PieceType).WithState(pieceSerializedData.State);
        }

        public PieceSerializedData From([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return
                new PieceSerializedData
                {
                    PieceType = piece.Type,
                    State = piece.State is null ? null : new Dictionary<string, string>(piece.State)
                };
        }
    }
}