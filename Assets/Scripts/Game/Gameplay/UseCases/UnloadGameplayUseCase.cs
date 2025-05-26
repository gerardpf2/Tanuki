using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Player;
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

        public UnloadGameplayUseCase(
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView,
            [NotNull] IPlayerView playerView)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);
            ArgumentNullException.ThrowIfNull(playerView);

            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
            _playerView = playerView;
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

            // TODO
            // _cameraController
            // _eventListener
        }

        private void UnloadScreen()
        {
            // TODO
            // _screenLoader
        }
    }
}