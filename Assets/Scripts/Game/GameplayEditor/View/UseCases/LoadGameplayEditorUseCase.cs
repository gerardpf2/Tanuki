using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.View.UseCases
{
    public class LoadGameplayEditorUseCase : ILoadGameplayEditorUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayEditorUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            PrepareModel();
            PrepareView();
            LoadScreen();
        }

        private static void PrepareModel()
        {
            // TODO
        }

        private static void PrepareView()
        {
            // TODO
        }

        private void LoadScreen()
        {
            GameplayEditorViewData gameplayViewData = new(OnReady);

            _screenLoader.Load(GameplayEditorConstants.ScreenKey, gameplayViewData);
        }

        private static void OnReady()
        {
            // TODO
        }
    }
}