using System;
using Game.Gameplay.Board;
using Game.Gameplay.Pieces;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Board
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