using System;
using Game.Gameplay.Board;
using UnityEngine;

namespace Game.Gameplay.Goals
{
    [Serializable]
    public class GoalDefinition : IGoalDefinition
    {
        [SerializeField] private PieceType _pieceType;
        [SerializeField] private int _amount;

        public PieceType PieceType => _pieceType;

        public int Amount => _amount;
    }
}