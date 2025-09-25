using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoard
    {
        event Action OnHighestNonEmptyRowUpdated;

        int Rows { get; }

        int Columns { get; }

        int HighestNonEmptyRow { get; }

        [NotNull] // Distinct pieces
        IDictionary<IPiece, Coordinate> PieceSourceCoordinates { get; }

        IPiece Get(Coordinate coordinate);

        void Add(IPiece piece, Coordinate sourceCoordinate);

        void Remove(IPiece piece);

        void Move(IPiece piece, int rowOffset, int columnOffset);
    }
}