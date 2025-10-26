using System.Collections.Generic;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoard
    {
        int Rows { get; }

        int Columns { get; }

        int HighestNonEmptyRow { get; }

        [NotNull]
        IEnumerable<int> PieceIds { get; }

        [NotNull]
        IPiece GetPiece(int pieceId);

        Coordinate GetSourceCoordinate(int pieceId);

        int? GetPieceId(Coordinate coordinate);

        void AddPiece(IPiece piece, Coordinate sourceCoordinate);

        void RemovePiece(int pieceId);

        void MovePiece(int pieceId, int rowOffset, int columnOffset);
    }
}