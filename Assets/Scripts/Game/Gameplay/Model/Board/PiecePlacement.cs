using System;
using Infrastructure.System;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Model.Board
{
    [Serializable]
    public class PiecePlacement : IPiecePlacement
    {
        [SerializeField] private PieceType _pieceType;
        [SerializeField, Min(0)] private int _row;
        [SerializeField, Min(0)] private int _column;

        public PieceType PieceType => _pieceType;

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        public int Row
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_row, ComparisonOperator.GreaterThanOrEqualTo, 0);

                return _row;
            }
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        public int Column
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_column, ComparisonOperator.GreaterThanOrEqualTo, 0);

                return _column;
            }
        }
    }
}