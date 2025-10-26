using System;
using System.Collections.Generic;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.Goals
{
    public class GoalViewData
    {
        public readonly PieceType PieceType;
        public readonly int InitialAmount;
        public readonly int CurrentAmount;

        public GoalViewData(PieceType pieceType, int initialAmount, int currentAmount)
        {
            PieceType = pieceType;
            InitialAmount = initialAmount;
            CurrentAmount = currentAmount;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not GoalViewData other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PieceType);
        }

        private bool Equals([NotNull] GoalViewData other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return EqualityComparer<PieceType>.Default.Equals(PieceType, other.PieceType);
        }
    }
}