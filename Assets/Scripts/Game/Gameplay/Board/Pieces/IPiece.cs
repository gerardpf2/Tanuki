using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public interface IPiece
    {
        PieceType Type { get; }

        bool Alive { get; }

        IEnumerable<KeyValuePair<string, string>> CustomData { get; }

        [NotNull]
        IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);
    }
}