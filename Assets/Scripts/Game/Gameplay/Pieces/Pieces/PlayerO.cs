using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerO : RectangularPiece
    {
        public override bool CanRotate => false;

        public override PieceType? DecomposeType => PieceType.BlockO;

        public PlayerO([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerO, 2, 2) { }

        public override IPiece Clone()
        {
            return new PlayerO(Converter, Id).WithState(State);
        }
    }
}