using System;
using Game.Common.Pieces;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces
{
    [Serializable]
    public class PieceViewDefinition : IPieceViewDefinition
    {
        [SerializeField] private PieceType _pieceType;
        [SerializeField] private GameObject _prefab;

        public PieceType PieceType => _pieceType;

        public GameObject Prefab
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_prefab);

                return _prefab;
            }
        }
    }
}