using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Header.Moves;
using Game.Gameplay.View.Input;
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
        [NotNull] private readonly IGoalsContainer _goalsContainer;
        [NotNull] private readonly IMovesContainer _movesContainer;
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly ICameraView _cameraView;
        [NotNull] private readonly IGoalsView _goalsView;
        [NotNull] private readonly IMovesView _movesView;
        [NotNull] private readonly IInputHandler _inputHandler;
        [NotNull] private readonly IPlayerPieceView _playerPieceView;
        [NotNull] private readonly IEventsResolver _eventsResolver;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public UnloadGameplayUseCase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] IPieceIdGetter pieceIdGetter,
            [NotNull] IGoalsContainer goalsContainer,
            [NotNull] IMovesContainer movesContainer,
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] ICameraView cameraView,
            [NotNull] IGoalsView goalsView,
            [NotNull] IMovesView movesView,
            [NotNull] IInputHandler inputHandler,
            [NotNull] IPlayerPieceView playerPieceView,
            [NotNull] IEventsResolver eventsResolver,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(pieceIdGetter);
            ArgumentNullException.ThrowIfNull(goalsContainer);
            ArgumentNullException.ThrowIfNull(movesContainer);
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(cameraView);
            ArgumentNullException.ThrowIfNull(goalsView);
            ArgumentNullException.ThrowIfNull(movesView);
            ArgumentNullException.ThrowIfNull(inputHandler);
            ArgumentNullException.ThrowIfNull(playerPieceView);
            ArgumentNullException.ThrowIfNull(eventsResolver);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _pieceIdGetter = pieceIdGetter;
            _goalsContainer = goalsContainer;
            _movesContainer = movesContainer;
            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _cameraView = cameraView;
            _goalsView = goalsView;
            _movesView = movesView;
            _inputHandler = inputHandler;
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
            _goalsContainer.Uninitialize();
            _movesContainer.Uninitialize();
            _phaseResolver.Uninitialize();
            _playerPiecesBag.Uninitialize();
        }

        private void PrepareView()
        {
            _boardView.Uninitialize();
            _cameraView.Uninitialize();
            _goalsView.Uninitialize();
            _movesView.Uninitialize();
            _inputHandler.Uninitialize();
            _playerPieceView.Uninitialize();
            _eventsResolver.Uninitialize();
        }

        private void UnloadScreen()
        {
            _screenLoader.Unload(GameplayConstants.ScreenKey);
        }
    }
}