using System;
using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Board
{
    [Serializable]
    public class BoardDefinition : IBoardDefinition
    {
        [SerializeField] private string _id;
        [SerializeField, Min(0)] private int _rows;
        [SerializeField, Min(0)] private int _columns;
        [NotNull, ItemNotNull, SerializeField] private List<PiecePlacement> _piecePlacements = new();

        public string Id => _id;

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        public int Rows
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_rows, ComparisonOperator.GreaterThanOrEqualTo, 0);

                return _rows;
            }
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        public int Columns
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_columns, ComparisonOperator.GreaterThanOrEqualTo, 0);

                return _columns;
            }
        }

        public IEnumerable<IPiecePlacement> PiecePlacements => _piecePlacements;
    }
}