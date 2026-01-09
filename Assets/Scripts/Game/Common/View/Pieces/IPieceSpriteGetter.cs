using Game.Common.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Common.View.Pieces
{
    public interface IPieceSpriteGetter
    {
        [NotNull]
        Sprite Get(PieceType pieceType);
    }
}