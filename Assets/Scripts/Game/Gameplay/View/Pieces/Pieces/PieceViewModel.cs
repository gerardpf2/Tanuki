using System;
using Game.Common;
using Game.Common.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator;
using Game.Gameplay.View.Animation.Animator.Utils;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity.Animator;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentException = Infrastructure.System.Exceptions.ArgumentException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class PieceViewModel : PieceViewModel<IPiece> { }

    public abstract class PieceViewModel<TPiece> : ViewModel, IDataSettable<IPiece>, IPieceViewInstantiateEventNotifier, IPieceViewRotateEventNotifier, IPieceViewDamageEventNotifier, IPieceViewMoveEventNotifier, IPieceViewHitEventNotifier, IAnimationEventNotifier where TPiece : IPiece
    {
        [SerializeField] private AnimatorTriggerNameContainer _animatorTriggerNameContainer;

        // State
        [NotNull] private readonly IBoundProperty<bool> _alive = new BoundProperty<bool>("Alive");
        [NotNull] private readonly IBoundProperty<Vector3> _offsetPosition = new BoundProperty<Vector3>("OffsetPosition");
        [NotNull] private readonly IBoundProperty<Quaternion> _offsetRotation = new BoundProperty<Quaternion>("OffsetRotation");
        // Animation
        [NotNull] private readonly IBoundTrigger<string> _animationTrigger = new BoundTrigger<string>("AnimationTrigger");

        protected TPiece Piece;

        private Action _animationOnComplete;

        protected virtual void Awake()
        {
            Add(_alive);
            Add(_offsetPosition);
            Add(_offsetRotation);
            Add(_animationTrigger);
        }

        public void SetData(IPiece data)
        {
            ArgumentException.ThrowIfTypeIsNot<TPiece>(data);

            Piece = (TPiece)data;

            SyncState();
        }

        public void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete)
        {
            // TODO

            onComplete?.Invoke();
        }

        public void OnRotated()
        {
            SyncRotation();
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
            PrepareMainAnimation(TriggerNameUtils.GetStart(movePieceReason), onComplete);
        }

        public void OnMovementEnded(MovePieceReason movePieceReason, Action onComplete)
        {
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

        public void OnAnimationEnd(string _)
        {
            /*
             *
             * Ideally animationName should be compared with the one that could have been set at PrepareAnimation in
             * order to determine if the animation that has ended is the expected one
             *
             * But this is something that cannot be done, at least for pieces, because one piece can use different
             * animations for each animation trigger and another can use the exact same animation for all them. In this
             * last case it is not clear which animation name should be set in the animator state
             *
             * This should only be called for main animations
             *
             */

            InvalidOperationException.ThrowIfNull(_animationOnComplete);

            _animationOnComplete();
            _animationOnComplete = null;
        }

        protected virtual void SyncState()
        {
            SyncAlive();
            SyncRotation();
        }

        protected void PrepareMainAnimation(string triggerName, Action onComplete)
        {
            InvalidOperationException.ThrowIfNull(_animatorTriggerNameContainer);
            InvalidOperationException.ThrowIfNotNull(_animationOnComplete);

            if (!_animatorTriggerNameContainer.Contains(triggerName))
            {
                // TODO: Exception

                onComplete?.Invoke();

                return;
            }

            _animationOnComplete = onComplete;
            _animationTrigger.Trigger(triggerName);
        }

        protected void PrepareMainAnimation(Action onComplete, params string[] triggerNames)
        {
            InvalidOperationException.ThrowIfNull(_animatorTriggerNameContainer);
            InvalidOperationException.ThrowIfNotNull(_animationOnComplete);

            foreach (string triggerName in triggerNames)
            {
                if (!_animatorTriggerNameContainer.Contains(triggerName))
                {
                    // TODO: Exception

                    onComplete?.Invoke();

                    return;
                }
            }

            _animationOnComplete = onComplete;

            foreach (string triggerName in triggerNames)
            {
                _animationTrigger.Trigger(triggerName);
            }
        }

        protected void PrepareSecondaryAnimation(string triggerName)
        {
            InvalidOperationException.ThrowIfNull(_animatorTriggerNameContainer);

            if (_animationOnComplete is not null)
            {
                // Main animation in progress

                return;
            }

            if (!_animatorTriggerNameContainer.Contains(triggerName))
            {
                return;
            }

            _animationTrigger.Trigger(triggerName);
        }

        protected void PrepareSecondaryAnimation(params string[] triggerNames)
        {
            InvalidOperationException.ThrowIfNull(_animatorTriggerNameContainer);

            if (_animationOnComplete is not null)
            {
                // Main animation in progress

                return;
            }

            foreach (string triggerName in triggerNames)
            {
                if (!_animatorTriggerNameContainer.Contains(triggerName))
                {
                    return;
                }
            }

            foreach (string triggerName in triggerNames)
            {
                _animationTrigger.Trigger(triggerName);
            }
        }

        private void SyncAlive()
        {
            InvalidOperationException.ThrowIfNull(Piece);

            _alive.Value = Piece.Alive;
        }

        private void SyncRotation()
        {
            InvalidOperationException.ThrowIfNull(Piece);

            _offsetPosition.Value = new Vector3(0.5f * (Piece.Width - 1), 0.5f * Piece.Height);
            _offsetRotation.Value = Quaternion.Euler(0.0f, 0.0f, -90.0f * Piece.Rotation); // Clockwise rotation
        }

        private Direction GetRotated(Direction direction)
        {
            InvalidOperationException.ThrowIfNull(Piece);

            return direction.GetRotated(Piece.Rotation);
        }
    }
}