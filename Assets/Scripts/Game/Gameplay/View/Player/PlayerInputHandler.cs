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

        private float _previousWorldPositionX;
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

            _previousWorldPositionX = GetWorldPositionX(eventData);
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

            float currentWorldPositionX = GetWorldPositionX(eventData);
            float deltaX = currentWorldPositionX - _previousWorldPositionX;

            _previousWorldPositionX = currentWorldPositionX;

            Move(deltaX);
        }

        public void OnEndDrag(PointerEventData _)
        {
            if (!_dragging)
            {
                InvalidOperationException.Throw(); // TODO
            }

            _dragging = false;
        }

        private static float GetWorldPositionX([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            return eventData.pointerCurrentRaycast.worldPosition.x;
        }

        private static bool IsInsideScreen(Vector2 position)
        {
            return position.x >= 0.0f && position.x <= Screen.width && position.y >= 0.0f && position.y <= Screen.height;
        }

        private void Move(float deltaX)
        {
            InvalidOperationException.ThrowIfNull(_playerView);

            _playerView.Move(deltaX);
        }
    }
}