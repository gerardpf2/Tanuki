using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplayScreen : ILoadGameplayScreen
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayScreen([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            _screenLoader.Load("Gameplay");
        }
    }
}