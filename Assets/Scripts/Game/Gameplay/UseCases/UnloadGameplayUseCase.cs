using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Player;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class UnloadGameplayUseCase : IUnloadGameplayUseCase
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;
        [NotNull] private readonly IPlayerView _playerView;
        [NotNull] private readonly ICameraController _cameraController;
        [NotNull] private readonly IEventListener _eventListener;
        [NotNull] private readonly IScreenLoader _screenLoader;

        public UnloadGameplayUseCase(
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] IPlayerView playerView,
            [NotNull] ICameraController cameraController,
            [NotNull] IEventListener eventListener,
            [NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(playerView);
            ArgumentNullException.ThrowIfNull(cameraController);
            ArgumentNullException.ThrowIfNull(eventListener);
            ArgumentNullException.ThrowIfNull(screenLoader);

            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _playerView = playerView;
            _cameraController = cameraController;
            _eventListener = eventListener;
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
            _phaseResolver.Uninitialize();
            _playerPiecesBag.Uninitialize();
        }

        private void PrepareView()
        {
            _boardView.Uninitialize();
            _playerView.Uninitialize();
            _cameraController.Uninitialize();
            _eventListener.Uninitialize();
        }

        private void UnloadScreen()
        {
            _screenLoader.Unload(GameplayConstants.ScreenKey);
        }
    }
}