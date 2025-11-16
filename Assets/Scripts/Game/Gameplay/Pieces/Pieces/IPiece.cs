using System.Collections.Generic;
using Game.Common.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public interface IPiece
    {
        int Id { get; }

        PieceType Type { get; }

        int Height { get; }

        int Width { get; }

        bool Alive { get; }

        IEnumerable<KeyValuePair<string, string>> State { get; }

        bool CanRotate { get; }

        int Rotation { get; set; } // Rotation steps 0, 1, 2, 3 -> 0, 90, 180, 270

        PieceType? DecomposeType { get; } // Expected to be a 1 Row x 1 Column piece, otherwise it may not work

        bool IsFilled(int rowOffset, int columnOffset);

        bool IsDamaged(int rowOffset, int columnOffset);

        void ProcessState(IEnumerable<KeyValuePair<string, string>> state);

        void Damage(int rowOffset, int columnOffset);

        [NotNull]
        IPiece Clone();
    }
}