using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.View.Board.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete);

        void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete);

        void OnDamaged(Action onComplete); // TODO: Add reason

        void OnMoved(MovePieceReason movePieceReason, Action onComplete);
    }
}