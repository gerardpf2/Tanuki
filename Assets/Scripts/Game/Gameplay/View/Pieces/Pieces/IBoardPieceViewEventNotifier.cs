using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public interface IBoardPieceViewEventNotifier : IPieceViewEventNotifier
    {
        void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete);

        void OnStartMovement(MovePieceReason movePieceReason, Action onComplete);

        void OnEndMovement(MovePieceReason movePieceReason, Action onComplete);

        void OnHit(); // TODO: Reason and direction
    }
}