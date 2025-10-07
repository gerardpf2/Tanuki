using System;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.View.Board.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete);

        void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete);

        void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete);

        void OnStartMove(MovePieceReason movePieceReason);

        void OnEndMove(Action onComplete);
    }
}