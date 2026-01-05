using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.MainMenu.UseCases
{
    public class LoadMainMenuUseCase : ILoadMainMenuUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadMainMenuUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            _screenLoader.Load(MainMenuConstants.ScreenKey);
        }
    }
}