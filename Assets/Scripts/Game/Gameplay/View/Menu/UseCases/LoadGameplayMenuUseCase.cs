using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Menu.UseCases
{
    public class LoadGameplayMenuUseCase : ILoadGameplayMenuUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayMenuUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve()
        {
            _screenLoader.Load("GameplayMenu"); // TODO
        }
    }
}