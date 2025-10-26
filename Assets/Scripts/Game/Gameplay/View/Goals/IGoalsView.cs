using System;
using System.Collections.Generic;
using Game.Gameplay.Goals;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Goals
{
    public interface IGoalsView
    {
        event Action OnUpdated;

        IEnumerable<PieceType> PieceTypes { get; }

        void Initialize();

        void Uninitialize();

        [NotNull]
        IGoal Get(PieceType pieceType);

        void SetCurrentAmount(PieceType pieceType, int currentAmount);
    }
}