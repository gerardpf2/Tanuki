using Game.Gameplay.PhaseResolution;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class UnloadGameplayUseCase : IUnloadGameplayUseCase
    {
        [NotNull] private readonly IPhaseResolver _phaseResolver;

        public UnloadGameplayUseCase([NotNull] IPhaseResolver phaseResolver)
        {
            ArgumentNullException.ThrowIfNull(phaseResolver);

            _phaseResolver = phaseResolver;
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

            // TODO
            // _playerPiecesBag
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