using System;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces
{
    [CreateAssetMenu(fileName = nameof(PieceSpriteContainer), menuName = "Tanuki/Game/Common/Pieces/" + nameof(PieceSpriteContainer))]
    public class PieceSpriteContainer : ScriptableObject
    {
        [Serializable]
        private class PieceTypeSpritePair
        {
            [SerializeField] private PieceType _pieceType;
            [SerializeField] private Sprite _sprite;

            public PieceType PieceType => _pieceType;

            public Sprite Sprite => _sprite;
        }

        [SerializeField] private PieceTypeSpritePair[] _pieceTypeSpritePairs;

        [NotNull]
        public Sprite Get(PieceType pieceType)
        {
            InvalidOperationException.ThrowIfNull(_pieceTypeSpritePairs);

            PieceTypeSpritePair pieceTypeSpritePair = null;

            foreach (PieceTypeSpritePair pieceTypeSpritePairCandidate in _pieceTypeSpritePairs)
            {
                InvalidOperationException.ThrowIfNull(pieceTypeSpritePairCandidate);

                if (pieceTypeSpritePairCandidate.PieceType != pieceType)
                {
                    continue;
                }

                pieceTypeSpritePair = pieceTypeSpritePairCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                pieceTypeSpritePair?.Sprite,
                $"Cannot get piece sprite with PieceType: {pieceType}"
            );

            return pieceTypeSpritePair.Sprite;
        }
    }
}