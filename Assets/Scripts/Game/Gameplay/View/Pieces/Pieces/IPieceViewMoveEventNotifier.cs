using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public interface IPieceViewMoveEventNotifier
    {
        void OnMovementStarted(MovePieceReason movePieceReason, Action onComplete);

        void OnMovementEnded(MovePieceReason movePieceReason, Action onComplete);
    }
}