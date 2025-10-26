using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public interface IPiece
    {
        int Id { get; }

        PieceType Type { get; }

        int Width { get; }

        int Height { get; }

        bool Alive { get; }

        IEnumerable<KeyValuePair<string, string>> State { get; }

        int Rotation { get; set; } // Rotation steps 0, 1, 2, 3 -> 0, 90, 180, 270

        bool IsFilled(int rowOffset, int columnOffset);

        void ProcessState(IEnumerable<KeyValuePair<string, string>> state);

        void Damage(int rowOffset, int columnOffset);

        [NotNull]
        IPiece Clone();
    }
}