using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.UseCases
{
    public class UnloadPauseMenuUseCase : IUnloadPauseMenuUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public UnloadPauseMenuUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            _screenLoader.Unload(PauseMenuConstants.ScreenKey);
        }
    }
}