using Game.Gameplay.Pieces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Pieces
{
    public interface IPieceViewDefinition
    {
        PieceType PieceType { get; }

        [NotNull]
        GameObject Prefab { get; }
    }
}