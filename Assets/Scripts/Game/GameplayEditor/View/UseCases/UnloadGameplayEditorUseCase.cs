using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.View.UseCases
{
    public class UnloadGameplayEditorUseCase : IUnloadGameplayEditorUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public UnloadGameplayEditorUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            PrepareModel();
            PrepareView();
            UnloadScreen();
        }

        private static void PrepareModel()
        {
            // TODO
        }

        private static void PrepareView()
        {
            // TODO
        }

        private void UnloadScreen()
        {
            _screenLoader.Unload(GameplayEditorConstants.ScreenKey);
        }
    }
}