using Game.Gameplay.View;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplay : ILoadGameplay
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplay([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve(string boardId)
        {
            _screenLoader.Load("Gameplay", new GameplayViewData(boardId));
        }
    }
}