using System;
using Game.Common;
using Game.Common.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator.Utils;
using Infrastructure.System;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class BoardPieceViewModel : BoardPieceViewModel<IPiece> { }

    public abstract class BoardPieceViewModel<TPiece> : PieceViewModel<TPiece>, IPieceViewDamageEventNotifier, IPieceViewMoveEventNotifier, IPieceViewHitEventNotifier where TPiece : IPiece
    {
        public void OnDamaged(DamagePieceReason damagePieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnMovementStarted(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.UnequalTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetStart(movePieceReason), onComplete);
        }

        public void OnMovementEnded(MovePieceReason movePieceReason, Action onComplete)
        {
            InvalidOperationException.ThrowIfNot(movePieceReason, ComparisonOperator.UnequalTo, MovePieceReason.Lock);

            PrepareMainAnimation(TriggerNameUtils.GetEnd(movePieceReason), onComplete);
        }

        public void OnHit(HitPieceReason hitPieceReason, Direction direction)
        {
            IPiece piece = Piece;

            InvalidOperationException.ThrowIfNull(piece);

            direction = direction.GetRotated(piece.Rotation);

            /*
             *
             * Maybe not ideal, but very convenient in terms of animator transition complexity
             * This secondary animation is split into three steps
             *
             * 1) Go from current secondary animation to hit animation selector
             * 2) Go from hit animation selector to hit strong / weak animation selector
             * 3) Go from hit strong / weak animation selector to direction down, right or left hit selector
             *
             * Triggers are raised in reverse order
             *
             */

            RaiseSecondaryAnimationTrigger(TriggerNameUtils.Get(hitPieceReason, direction));
            RaiseSecondaryAnimationTrigger(TriggerNameUtils.Get(hitPieceReason));
            RaiseSecondaryAnimationTrigger(TriggerNameUtils.GetHitBase());
        }
    }
}