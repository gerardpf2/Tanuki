using System;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public interface IPieceViewDamageEventNotifier
    {
        void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete);
    }
}