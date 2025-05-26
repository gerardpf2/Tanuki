namespace Game.Gameplay.UseCases
{
    public class UnloadGameplayUseCase : IUnloadGameplayUseCase
    {
        public void Resolve()
        {
            PrepareModel();
            PrepareView();
            UnloadScreen();
        }

        private void PrepareModel()
        {
            // _phaseResolver
            // _playerPiecesBag
        }

        private void PrepareView()
        {
            // _boardView
            // _playerView
            // _cameraController
            // _eventListener
        }

        private void UnloadScreen()
        {
            // _screenLoader
        }
    }
}