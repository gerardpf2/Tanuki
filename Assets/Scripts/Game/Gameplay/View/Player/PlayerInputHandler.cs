using Game.Gameplay.PhaseResolution;
using Game.Gameplay.View.EventResolution;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Player
{
    public class PlayerInputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float LockPieceDeltaY = -0.5f; // TODO: Scriptable object for this and other input params

        private IPhaseResolver _phaseResolver;
        private IReadonlyEventsResolver _eventsResolver;
        private IPlayerView _playerView;
        private IScreenPropertiesGetter _screenPropertiesGetter;

        private Vector2 _previousWorldPosition;
        private bool _dragging;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IReadonlyEventsResolver eventsResolver,
            [NotNull] IPlayerView playerView,
            [NotNull] IScreenPropertiesGetter screenPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(playerView);
            ArgumentNullException.ThrowIfNull(screenPropertiesGetter);

            _phaseResolver = phaseResolver;
            _eventsResolver = eventsResolver;
            _playerView = playerView;
            _screenPropertiesGetter = screenPropertiesGetter;
        }

        public void OnBeginDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            if (_dragging)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _previousWorldPosition = GetWorldPosition(eventData);
            _dragging = true;
        }

        public void OnDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            if (!_dragging)
            {
                InvalidOperationException.Throw(); // TODO
            }

            if (!IsInsideScreen(eventData.position))
            {
                return;
            }

            Vector2 currentWorldPosition = GetWorldPosition(eventData);
            Vector2 worldPositionDelta = currentWorldPosition - _previousWorldPosition;

            _previousWorldPosition = currentWorldPosition;

            HandleDrag(worldPositionDelta);
        }

        public void OnEndDrag(PointerEventData _)
        {
            if (!_dragging)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _dragging = false;
        }

        private static Vector2 GetWorldPosition([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            return eventData.pointerCurrentRaycast.worldPosition;
        }

        private bool IsInsideScreen(Vector2 position)
        {
            InvalidOperationException.ThrowIfNull(_screenPropertiesGetter);

            return
                position.x >= 0.0f && position.x <= _screenPropertiesGetter.Width &&
                position.y >= 0.0f && position.y <= _screenPropertiesGetter.Height;
        }

        private void HandleDrag(Vector2 worldPositionDelta)
        {
            InvalidOperationException.ThrowIfNull(_phaseResolver);
            InvalidOperationException.ThrowIfNull(_eventsResolver);
            InvalidOperationException.ThrowIfNull(_playerView);

            if (_eventsResolver.Resolving)
            {
                return;
            }

            if (worldPositionDelta.y <= LockPieceDeltaY)
            {
                _phaseResolver.Resolve();
            }
            else
            {
                _playerView.Move(worldPositionDelta.x);
            }
        }
    }
}