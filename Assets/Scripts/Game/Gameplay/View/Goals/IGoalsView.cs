using System;
using System.Collections.Generic;
using Game.Common.Pieces;
using Game.Gameplay.Goals;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Goals
{
    public interface IGoalsView
    {
        event Action OnUpdated;

        [NotNull, ItemNotNull]
        IEnumerable<IGoal> Entries { get; }

        void Initialize();

        void Uninitialize();

        void SetCurrentAmount(PieceType pieceType, int currentAmount);
    }
}