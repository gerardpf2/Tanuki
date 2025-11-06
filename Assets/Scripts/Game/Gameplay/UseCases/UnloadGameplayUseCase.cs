using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Phases;
using Game.Gameplay.Pieces;
using Game.Gameplay.REMOVE;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Player;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class UnloadGameplayUseCase : IUnloadGameplayUseCase
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IMovesContainer _movesContainer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IGameplaySerializerOnBeginIteration _gameplaySerializerOnBeginIteration;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IPlayerPieceGhostView _playerPieceGhostView;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public UnloadGameplayUseCase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] ICamera camera,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IMovesContainer movesContainer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IGameplaySerializerOnBeginIteration gameplaySerializerOnBeginIteration,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
            [NotNull] IPlayerPieceGhostView playerPieceGhostView,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(movesContainer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(gameplaySerializerOnBeginIteration);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
            ArgumentNullException.ThrowIfNull(playerPieceGhostView);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _pieceIdGetter = pieceIdGetter;
            _camera = camera;
            _goalsContainer = goalsContainer;
            _movesContainer = movesContainer;
            _phaseResolver = phaseResolver;
            _gameplaySerializerOnBeginIteration = gameplaySerializerOnBeginIteration;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _playerPieceGhostView = playerPieceGhostView;
            _playerPieceView = playerPieceView;
            _eventsResolver = eventsResolver;
            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            PrepareModel();
            PrepareView();
            UnloadScreen();
        }

        private void PrepareModel()
        {
            _bagContainer.Uninitialize();
            _boardContainer.Uninitialize();
            _pieceIdGetter.Uninitialize();
            _camera.Uninitialize();
            _goalsContainer.Uninitialize();
            _movesContainer.Uninitialize();
            _phaseResolver.Uninitialize();
            _gameplaySerializerOnBeginIteration.Uninitialize();
        }

        private void PrepareView()
        {
            _boardView.Uninitialize();
            _cameraView.Uninitialize();
            _goalsView.Uninitialize();
            _movesView.Uninitialize();
            _playerPieceGhostView.Uninitialize();
            _playerPieceView.Uninitialize();
            _eventsResolver.Uninitialize();
        }

        private void UnloadScreen()
        {
            _screenLoader.Unload(GameplayConstants.ScreenKey);
        }
    }
}