using System;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsViewContainer : GoalsContainer, IGoalsViewContainer
    {
        public event Action<PieceType> OnUpdated;

        protected override void HandleCurrentUpdated(PieceType pieceType)
        {
            base.HandleCurrentUpdated(pieceType);

            OnUpdated?.Invoke(pieceType);
        }
    }
}