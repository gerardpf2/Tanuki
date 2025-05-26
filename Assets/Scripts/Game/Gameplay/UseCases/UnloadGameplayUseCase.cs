using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class UnloadGameplayUseCase : IUnloadGameplayUseCase
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;
        [NotNull] private readonly IBoardView _boardView;

        public UnloadGameplayUseCase(
            [NotNull] IPhaseResolver phaseResolver,
            [NotNull] IPlayerPiecesBag playerPiecesBag,
            [NotNull] IBoardView boardView)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);
            ArgumentNullException.ThrowIfNull(boardView);

            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
            _boardView = boardView;
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

            // TODO
            // _playerView
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