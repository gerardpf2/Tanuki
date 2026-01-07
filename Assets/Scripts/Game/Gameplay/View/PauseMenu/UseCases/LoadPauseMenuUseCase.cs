using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.UseCases
{
    public class LoadPauseMenuUseCase : ILoadPauseMenuUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadPauseMenuUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            _screenLoader.Load(PauseMenuConstants.ScreenKey);
        }
    }
}