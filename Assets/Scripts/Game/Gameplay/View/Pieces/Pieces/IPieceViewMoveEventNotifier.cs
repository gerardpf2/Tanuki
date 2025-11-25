using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public interface IPieceViewMoveEventNotifier
    {
        void OnStartMovement(MovePieceReason movePieceReason, Action onComplete);

        void OnEndMovement(MovePieceReason movePieceReason, Action onComplete);
    }
}