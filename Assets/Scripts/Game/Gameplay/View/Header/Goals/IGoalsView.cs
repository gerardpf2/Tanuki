using System;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;

namespace Game.Gameplay.View.Header.Goals
{
    public interface IGoalsView
    {
        event Action OnUpdated;

        IGoals Goals { get; }

        void Initialize();

        void Uninitialize();

        void SetCurrentAmount(PieceType pieceType, int currentAmount);
    }
}