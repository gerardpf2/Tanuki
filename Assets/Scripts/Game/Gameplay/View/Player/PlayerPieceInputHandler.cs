using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceInputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private const float LockPieceDeltaY = -0.5f; // TODO: Scriptable object for this and other input params

        private IPhaseContainer _phaseContainer;
        private IEventsResolver _eventsResolver;
        private IPlayerPieceGhostView _playerPieceGhostView;
        private IPlayerPieceView _playerPieceView;
        private IScreenPropertiesGetter _screenPropertiesGetter;

        private Vector2 _previousWorldPosition;
        private bool _waitingEndDrag;

        private void Awake()
        {
            InjectResolver.Resolve(this);
        }

        public void Inject(
            [NotNull] IPhaseContainer phaseContainer,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IScreenPropertiesGetter screenPropertiesGetter)
        {
            ArgumentNullException.ThrowIfNull(phaseContainer);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(screenPropertiesGetter);

            _phaseContainer = phaseContainer;
            _eventsResolver = eventsResolver;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
            _screenPropertiesGetter = screenPropertiesGetter;
        }

        public void OnBeginDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            _previousWorldPosition = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnDrag([NotNull] PointerEventData eventData)
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

        public void OnEndDrag(PointerEventData _)
        {
            _waitingEndDrag = false;
        }

        public void OnPointerClick([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            if (!CanClick(eventData))
            {
                return;
            }

            HandlePointerClick();
        }

        private bool CanDrag([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            return CanPerformAnyAction() && !_waitingEndDrag && IsInsideScreen(eventData.position);
        }

        private bool CanClick([NotNull] PointerEventData eventData)
        {
            ArgumentNullException.ThrowIfNull(eventData);

            return CanPerformAnyAction() && !eventData.dragging;
        }

        private bool CanPerformAnyAction()
        {
            InvalidOperationException.ThrowIfNull(_eventsResolver);
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            return !_eventsResolver.Resolving && _playerPieceView.Instance != null;
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
            InvalidOperationException.ThrowIfNull(_phaseContainer);
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Move(worldPositionDelta.x);

            if (worldPositionDelta.y > LockPieceDeltaY)
            {
                return;
            }

            _phaseContainer.Resolve(new ResolveContext(_playerPieceView.Coordinate, _playerPieceGhostView.Coordinate));

            _waitingEndDrag = true;
        }

        private void HandlePointerClick()
        {
            InvalidOperationException.ThrowIfNull(_playerPieceView);

            _playerPieceView.Rotate();
        }
    }
}