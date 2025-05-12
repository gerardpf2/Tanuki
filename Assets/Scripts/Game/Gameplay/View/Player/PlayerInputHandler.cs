using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Player
{
    public class PlayerInputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IPlayerView _playerView;

        private Vector2 _previousWorldPosition;
        private bool _dragging;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(playerView);

            _playerView = playerView;
        }

        public void OnBeginDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            if (_dragging)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _dragging = true;
            _previousWorldPosition = GetWorldPosition(eventData);
        }

        public void OnDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);
            InvalidOperationException.ThrowIfNull(_playerView);

            if (!_dragging)
            {
                InvalidOperationException.Throw(); // TODO
            }

            if (IsOutsideScreen(eventData))
            {
                return;
            }

            Vector2 currentWorldPosition = GetWorldPosition(eventData);
            float deltaX = (currentWorldPosition - _previousWorldPosition).x;

            _playerView.Move(deltaX);

            _previousWorldPosition = currentWorldPosition;
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

        private static bool IsOutsideScreen([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            Vector2 position = eventData.position;

            return position.x < 0.0f || position.x > Screen.width || position.y < 0.0f || position.y > Screen.height;
        }
    }
}