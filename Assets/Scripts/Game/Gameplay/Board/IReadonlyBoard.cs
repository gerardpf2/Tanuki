using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IReadonlyBoard
    {
        event Action OnHighestNonEmptyRowUpdated;

        int Rows { get; }

        int Columns { get; }

        int HighestNonEmptyRow { get; }

        [NotNull] // Distinct pieces
        IDictionary<IPiece, Coordinate> PieceSourceCoordinates { get; }

        IPiece Get(Coordinate coordinate);
    }
}