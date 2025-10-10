using Game.Common;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Player;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Input
{
    public class InputHandler : IInputHandler
    {
        private const float LockPieceDeltaY = -0.5f; // TODO: Scriptable object for this and other input params

        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IInputListener _inputListener;
        [NotNull] private readonly IPiecePlayerView _piecePlayerView;
        [NotNull] private readonly IScreenPropertiesGetter _screenPropertiesGetter;

        private InitializedLabel _initializedLabel;

        private Vector2 _previousWorldPosition;
        private bool _waitingDragEnd;

        public InputHandler(
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IInputListener inputListener,
            [NotNull] IPiecePlayerView piecePlayerView,
            [NotNull] IScreenPropertiesGetter screenPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(inputListener);
            ArgumentNullException.ThrowIfNull(piecePlayerView);
            ArgumentNullException.ThrowIfNull(screenPropertiesGetter);

            _phaseResolver = phaseResolver;
            _eventsResolver = eventsResolver;
            _inputListener = inputListener;
            _piecePlayerView = piecePlayerView;
            _screenPropertiesGetter = screenPropertiesGetter;
        }

        public void Initialize()
        {
            _initializedLabel.SetInitialized();

            SubscribeToEvents();
        }

        public void Uninitialize()
        {
            _initializedLabel.SetUninitialized();

            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _inputListener.OnBeginDrag += HandleBeginDrag;
            _inputListener.OnDrag += HandleDrag;
            _inputListener.OnEndDrag += HandleEndDrag;
        }

        private void UnsubscribeFromEvents()
        {
            _inputListener.OnBeginDrag -= HandleBeginDrag;
            _inputListener.OnDrag -= HandleDrag;
            _inputListener.OnEndDrag -= HandleEndDrag;
        }

        private void HandleBeginDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            _previousWorldPosition = eventData.pointerCurrentRaycast.worldPosition;
        }

        private void HandleDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            if (!CanDrag(eventData))
            {
                return;
            }

            Vector2 currentWorldPosition = eventData.pointerCurrentRaycast.worldPosition;
            Vector2 worldPositionDelta = currentWorldPosition - _previousWorldPosition;

            _previousWorldPosition = currentWorldPosition;

            HandleDrag(worldPositionDelta);
        }

        private void HandleEndDrag(PointerEventData _)
        {
            _waitingDragEnd = false;
        }

        private bool CanDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            return !_eventsResolver.Resolving && !_waitingDragEnd && IsInsideScreen(eventData.position);
        }

        private bool IsInsideScreen(Vector2 position)
        {
            return
                position.x >= 0.0f && position.x <= _screenPropertiesGetter.Width &&
                position.y >= 0.0f && position.y <= _screenPropertiesGetter.Height;
        }

        private void HandleDrag(Vector2 worldPositionDelta)
        {
            _piecePlayerView.Move(worldPositionDelta.x);

            if (worldPositionDelta.y > LockPieceDeltaY)
            {
                return;
            }

            _phaseResolver.Resolve(new ResolveContext(_piecePlayerView.Coordinate.Column));

            _waitingDragEnd = true;
        }
    }
}