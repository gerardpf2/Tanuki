using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.EventNotifiers
{
    public interface IPieceViewMoveEventNotifier
    {
        void OnMovementStarted(MovePieceReason movePieceReason, Action onComplete);

        void OnMovementEnded(Action onComplete);
    }
}