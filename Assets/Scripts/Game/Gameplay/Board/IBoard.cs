using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoard
    {
        int Rows { get; }

        int Columns { get; }

        int HighestNonEmptyRow { get; }

        [NotNull]
        IEnumerable<int> Ids { get; }

        [NotNull]
        IPiece Get(int id);

        Coordinate GetSourceCoordinate(int id);

        int? Get(Coordinate coordinate);

        void Add(IPiece piece, Coordinate sourceCoordinate);

        void Remove(int id);

        void Move(int id, int rowOffset, int columnOffset);
    }
}