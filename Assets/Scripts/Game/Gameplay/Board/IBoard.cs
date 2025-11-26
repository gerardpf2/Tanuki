using System.Collections.Generic;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoard
    {
        int HighestNonEmptyRow { get; }

        int Columns { get; }

        [NotNull]
        IEnumerable<int> PieceIds { get; }

        void Build(int columns);

        void Clear();

        bool IsInside(Coordinate coordinate);

        [NotNull]
        IPiece GetPiece(int pieceId);

        Coordinate GetSourceCoordinate(int pieceId);

        int? GetPieceId(Coordinate coordinate);

        void AddPiece(IPiece piece, Coordinate sourceCoordinate);

        void RemovePiece(int pieceId);

        void MovePiece(int pieceId, int rowOffset, int columnOffset);
    }
}