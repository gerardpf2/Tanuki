using System;
using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header.Goals
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