using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Board.Pieces
{
    public interface IPieceViewEventNotifier
    {
        void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete);

        void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete);

        void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete);

        void OnStartMovement(MovePieceReason movePieceReason, Action onComplete);

        void OnEndMovement(MovePieceReason movePieceReason, Action onComplete);

        void OnRotated();
    }
}