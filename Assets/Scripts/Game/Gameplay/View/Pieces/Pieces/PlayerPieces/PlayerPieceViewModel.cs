using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Animation.Animator.Utils;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Infrastructure.System;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces.PlayerPieces
{
    public class PlayerPieceViewModel : PieceViewModel, IPieceViewMoveEventNotifier
    {
        public void OnMovementStarted(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.EqualTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetStart(movePieceReason), onComplete);
        }

        public void OnMovementEnded(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.EqualTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetEnd(movePieceReason), onComplete);
        }
    }
}