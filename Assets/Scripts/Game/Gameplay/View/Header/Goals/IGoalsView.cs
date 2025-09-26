using System;
using Game.Gameplay.Board;

namespace Game.Gameplay.View.Header.Goals
{
    public interface IGoalsView
    {
        event Action OnUpdated;

        void Initialize();

        void Uninitialize();

        void TryIncreaseCurrentAmount(PieceType pieceType);
    }
}