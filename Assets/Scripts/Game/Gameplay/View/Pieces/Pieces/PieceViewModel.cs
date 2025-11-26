using System;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Animator;
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

    public abstract class PieceViewModel<TPiece> : ViewModel, IDataSettable<IPiece>, IPieceViewInstantiateEventNotifier, IPieceViewRotateEventNotifier, IAnimationEventNotifier where TPiece : IPiece
    {
        [SerializeField] private AnimatorTriggerNameContainer _animatorTriggerNameContainer;

        [NotNull] private readonly IBoundProperty<Vector3> _offsetPosition = new BoundProperty<Vector3>("OffsetPosition");
        [NotNull] private readonly IBoundProperty<Quaternion> _offsetRotation = new BoundProperty<Quaternion>("OffsetRotation");
        [NotNull] private readonly IBoundTrigger<string> _animationTrigger = new BoundTrigger<string>("AnimationTrigger");

        protected TPiece Piece;

        private Action _animationOnComplete;

        private void Awake()
        {
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

        protected void RaiseSecondaryAnimationTrigger(params string[] triggerNames)
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

        private void SyncRotation()
        {
            InvalidOperationException.ThrowIfNull(Piece);

            _offsetPosition.Value = new Vector3(0.5f * (Piece.Width - 1), 0.5f * Piece.Height);
            _offsetRotation.Value = Quaternion.Euler(0.0f, 0.0f, -90.0f * Piece.Rotation); // Clockwise rotation
        }
    }
}