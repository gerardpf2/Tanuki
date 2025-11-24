using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.ModelViewViewModel;
using Infrastructure.Unity.Animator;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentException = Infrastructure.System.Exceptions.ArgumentException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Pieces.Pieces
{
    public abstract class BasePieceViewModel<T> : ViewModel, IDataSettable<IPiece>, IPieceViewEventNotifier, IAnimationEventNotifier where T : IPiece
    {
        [NotNull] private readonly IBoundProperty<Vector3> _offsetPosition = new BoundProperty<Vector3>("OffsetPosition");
        [NotNull] private readonly IBoundProperty<Quaternion> _offsetRotation = new BoundProperty<Quaternion>("OffsetRotation");
        [NotNull] private readonly IBoundTrigger<string> _animationTrigger = new BoundTrigger<string>("AnimationTrigger");

        protected T Piece;

        private Action _animationOnComplete;

        private void Awake()
        {
            Add(_offsetPosition);
            Add(_offsetRotation);
            Add(_animationTrigger);
        }

        public void SetData(IPiece data)
        {
            ArgumentException.ThrowIfTypeIsNot<T>(data);

            Piece = (T)data;

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
             */

            _animationOnComplete?.Invoke();
            _animationOnComplete = null;
        }

        protected virtual void SyncState()
        {
            SyncRotation();
        }

        protected void PrepareMainAnimation(string triggerName, Action onComplete)
        {
            // TODO: Remove animator check when each piece has proper animator and animations

            Animator animator = GetComponentInChildren<Animator>();

            if (!animator || !animator.runtimeAnimatorController)
            {
                onComplete?.Invoke();

                return;
            }

            InvalidOperationException.ThrowIfNotNull(_animationOnComplete);

            _animationOnComplete = onComplete;
            _animationTrigger.Trigger(triggerName);
        }

        protected void PrepareSecondaryAnimation(string triggerName)
        {
            if (_animationOnComplete is not null)
            {
                // Main animation in progress

                return;
            }

            // TODO: Remove animator check when each piece has proper animator and animations

            Animator animator = GetComponentInChildren<Animator>();

            if (!animator || !animator.runtimeAnimatorController)
            {
                return;
            }

            _animationTrigger.Trigger(triggerName);
        }

        private void SyncRotation()
        {
            InvalidOperationException.ThrowIfNull(Piece);

            _offsetPosition.Value = new Vector3(0.5f * (Piece.Width - 1), 0.5f * Piece.Height);
            _offsetRotation.Value = Quaternion.Euler(0.0f, 0.0f, -90.0f * Piece.Rotation); // Clockwise rotation
        }
    }
}