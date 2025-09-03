using System;
using System.Collections.Generic;
using Game.Gameplay.Board;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Common.Pieces
{
    [CreateAssetMenu(fileName = nameof(PieceSpriteContainer), menuName = "Tanuki/Game/Common/Pieces/" + nameof(PieceSpriteContainer))]
    public class PieceSpriteContainer : ScriptableObject
    {
        [Serializable]
        private struct PieceTypeSpritePair
        {
            [SerializeField] private PieceType _pieceType;
            [SerializeField] private Sprite _sprite;

            public PieceType PieceType => _pieceType;

            public Sprite Sprite => _sprite;
        }

        [NotNull, SerializeField] private List<PieceTypeSpritePair> _pieceTypeSpritePairs = new();

        [NotNull]
        public Sprite Get(PieceType pieceType)
        {
            PieceTypeSpritePair pieceTypeSpritePair = _pieceTypeSpritePairs.Find(pieceTypeSpritePair => pieceTypeSpritePair.PieceType == pieceType);

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceTypeSpritePair.Sprite,
                $"Cannot get piece sprite with PieceType: {pieceType}"
            );

            return pieceTypeSpritePair.Sprite;
        }
    }
}