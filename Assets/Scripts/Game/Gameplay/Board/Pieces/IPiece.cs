using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public interface IPiece
    {
        int Id { get; }

        PieceType Type { get; }

        bool Alive { get; }

        IEnumerable<KeyValuePair<string, string>> State { get; }

        [NotNull]
        bool[,] Grid { get; }

        void ProcessState(IEnumerable<KeyValuePair<string, string>> state);

        void Damage(int rowOffset, int columnOffset);

        [NotNull]
        IPiece Clone();
    }
}