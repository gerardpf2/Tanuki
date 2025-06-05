using System;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;

namespace Game.Gameplay.View.Header.Goals
{
    public interface IGoalsViewContainer : IGoalsContainer
    {
        event Action<PieceType> OnUpdated;
    }
}