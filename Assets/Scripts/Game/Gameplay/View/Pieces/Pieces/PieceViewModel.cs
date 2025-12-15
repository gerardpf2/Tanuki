using System;
using System.Collections;
using Game.Common;
using Game.Common.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator;
using Game.Gameplay.View.Animation.Animator.Utils;
using Game.Gameplay.View.Pieces.EventNotifiers;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity.Animator;
using Infrastructure.Unity.Utils;
using JetBrains.Annotations;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public class PieceViewModel : PieceViewModel<IPiece> { }

    public abstract class PieceViewModel<TPiece> : ViewModel, IDataSettable<PieceViewData>, IPieceViewInstantiateEventNotifier, IPieceViewRotateEventNotifier, IPieceViewDamageEventNotifier, IPieceViewMoveEventNotifier, IPieceViewHitEventNotifier, IAnimationEventNotifier where TPiece : IPiece
    {
        [SerializeField] private AnimatorTriggerNameContainer _animatorTriggerNameContainer;

        [NotNull] private readonly IBoundProperty<Vector3> _offsetPosition = new BoundProperty<Vector3>("OffsetPosition");
        [NotNull] private readonly IBoundProperty<Quaternion> _offsetRotation = new BoundProperty<Quaternion>("OffsetRotation");
        [NotNull] private readonly IBoundTrigger<string> _animationTrigger = new BoundTrigger<string>("AnimationTrigger");

        private PieceViewData _pieceViewData;
        private Action _animationOnComplete;
        private bool _ready;

        protected TPiece Piece => _pieceViewData?.Piece is TPiece tPiece ? tPiece : default;

        private void Awake()
        {
            AddBindings();
        }

        private IEnumerator Start()
        {
            yield return CoroutineUtils.GetWaitForEndOfFrame(OnReady);
        }

        private void OnReady()
        {
            _ready = true;

            CallViewDataOnReadyIfNeeded();
        }

        public void SetData(PieceViewData data)
        {
            _pieceViewData = data;

            SyncState();

            CallViewDataOnReadyIfNeeded();
        }

        public void OnInstantiated(InstantiatePieceReason instantiatePieceReason, Action onComplete)
        {
            PrepareMainAnimation(
                onComplete,
                TriggerNameUtils.GetInstantiate(instantiatePieceReason),
                TriggerNameUtils.GetInstantiate()
            );
        }

        public void OnDestroyed(DestroyPieceReason destroyPieceReason, Action onComplete)
        {
            PrepareMainAnimation(
                onComplete,
                TriggerNameUtils.GetDestroy(destroyPieceReason),
                TriggerNameUtils.GetDestroy()
            );
        }

        public void OnRotated()
        {
            SyncRotation();
        }

        public void OnDamaged(DamagePieceReason damagePieceReason, Direction direction, Action onComplete)
        {
            direction = GetRotated(direction);

            PrepareMainAnimation(
                onComplete,
                TriggerNameUtils.GetDamage(damagePieceReason, direction),
                TriggerNameUtils.GetDamage(damagePieceReason),
                TriggerNameUtils.GetDamage()
            );
        }

        public void OnMovementStarted(MovePieceReason movePieceReason, Action onComplete)
        {
            PrepareMainAnimation(
                onComplete,
                TriggerNameUtils.GetMoveStart(movePieceReason),
                TriggerNameUtils.GetMove(movePieceReason),
                TriggerNameUtils.GetMove()
            );
        }

        public void OnMovementEnded(Action onComplete)
        {
            PrepareMainAnimation(TriggerNameUtils.GetMoveEnd(), onComplete);
        }

        public void OnHit(HitPieceReason hitPieceReason, Direction direction)
        {
            direction = GetRotated(direction);

            PrepareSecondaryAnimation(
                TriggerNameUtils.GetHit(hitPieceReason, direction),
                TriggerNameUtils.GetHit(hitPieceReason),
                TriggerNameUtils.GetHit()
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

            Action animationOnCompleteCopy = _animationOnComplete;

            _animationOnComplete = null;

            animationOnCompleteCopy();
        }

        protected virtual void AddBindings()
        {
            Add(_offsetPosition);
            Add(_offsetRotation);
            Add(_animationTrigger);
        }

        protected virtual void SyncState()
        {
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

        private void CallViewDataOnReadyIfNeeded()
        {
            if (_ready)
            {
                _pieceViewData?.OnReady?.Invoke();
            }
        }

        private void SyncRotation()
        {
            TPiece piece = Piece;

            InvalidOperationException.ThrowIfNull(piece);

            _offsetPosition.Value = new Vector3(0.5f * (piece.Width - 1), 0.5f * piece.Height);
            _offsetRotation.Value = Quaternion.Euler(0.0f, 0.0f, -90.0f * piece.Rotation); // Clockwise rotation
        }

        private Direction GetRotated(Direction direction)
        {
            TPiece piece = Piece;

            InvalidOperationException.ThrowIfNull(piece);

            return direction.GetRotated(piece.Rotation);
        }
    }
}