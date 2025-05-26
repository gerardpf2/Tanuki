using Game.Gameplay.PhaseResolution;
using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class UnloadGameplayUseCase : IUnloadGameplayUseCase
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        public UnloadGameplayUseCase([NotNull] IPhaseResolver phaseResolver, [NotNull] IPlayerPiecesBag playerPiecesBag)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _phaseResolver = phaseResolver;
            _playerPiecesBag = playerPiecesBag;
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
            // TODO
            // _boardView
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