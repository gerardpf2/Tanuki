using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
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
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IPieceIdGetter _pieceIdGetter;
        [NotNull] private readonly ICamera _camera;
        [NotNull] private readonly IGoals _goals;
        [NotNull] private readonly IMoves _moves;
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
            [NotNull] IBoard board,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] ICamera camera,
            [NotNull] IGoals goals,
            [NotNull] IMoves moves,
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
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(camera);
            ArgumentNullException.ThrowIfNull(goals);
            ArgumentNullException.ThrowIfNull(moves);
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
            _board = board;
            _pieceIdGetter = pieceIdGetter;
            _camera = camera;
            _goals = goals;
            _moves = moves;
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
            _pieceIdGetter.Uninitialize();
            _camera.Uninitialize();
            _gameplaySerializerOnBeginIteration.Uninitialize();

            _board.Clear();
            _goals.Clear();
            _moves.Reset();
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