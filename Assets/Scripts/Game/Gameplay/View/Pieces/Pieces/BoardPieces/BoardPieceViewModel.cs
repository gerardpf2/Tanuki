using System;
using Game.Common;
using Game.Common.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator.Utils;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System;
using JetBrains.Annotations;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces.BoardPieces
{
    public class BoardPieceViewModel : BoardPieceViewModel<IPiece> { }

    public abstract class BoardPieceViewModel<TPiece> : PieceViewModel<TPiece>, IPieceViewDamageEventNotifier, IPieceViewMoveEventNotifier, IPieceViewHitEventNotifier where TPiece : IPiece
    {
        [NotNull] private readonly IBoundProperty<bool> _alive = new BoundProperty<bool>("Alive");

        protected override void Awake()
        {
            base.Awake();

            Add(_alive);
        }

        public void OnDamaged(DamagePieceReason damagePieceReason, Direction direction, Action onComplete)
        {
            direction = GetRotated(direction);

            PrepareMainAnimation(
                OnComplete,
                TriggerNameUtils.Get(damagePieceReason, direction),
                TriggerNameUtils.Get(damagePieceReason),
                TriggerNameUtils.GetDamageBase()
            );

            return;

            void OnComplete()
            {
                SyncAlive();

                onComplete?.Invoke();
            }
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
            direction = GetRotated(direction);

            PrepareSecondaryAnimation(
                TriggerNameUtils.Get(hitPieceReason, direction),
                TriggerNameUtils.Get(hitPieceReason),
                TriggerNameUtils.GetHitBase()
            );
        }

        protected override void SyncState()
        {
            base.SyncState();

            SyncAlive();
        }

        private Direction GetRotated(Direction direction)
        {
            InvalidOperationException.ThrowIfNull(Piece);

            return direction.GetRotated(Piece.Rotation);
        }

        private void SyncAlive()
        {
            InvalidOperationException.ThrowIfNull(Piece);

            _alive.Value = Piece.Alive;
        }
    }
}