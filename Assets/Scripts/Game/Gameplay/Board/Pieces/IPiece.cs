using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public interface IPiece
    {
        uint Id { get; }

        PieceType Type { get; }

        bool Alive { get; }

        IEnumerable<KeyValuePair<string, string>> CustomData { get; }

        [NotNull]
        IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);

        void ProcessCustomData(IEnumerable<KeyValuePair<string, string>> customData);

        void Damage(int rowOffset, int columnOffset);

        [NotNull]
        IPiece Clone();
    }
}