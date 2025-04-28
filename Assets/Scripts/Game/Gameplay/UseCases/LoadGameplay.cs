using Game.Gameplay.View;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.UseCases
{
    public class LoadGameplay : ILoadGameplay
    {
        [NotNull] private readonly IScreenLoader _screenLoader;
        [NotNull] private readonly IGameplay _gameplay;

        public LoadGameplay([NotNull] IScreenLoader screenLoader, [NotNull] IGameplay gameplay)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);
            ArgumentNullException.ThrowIfNull(gameplay);

            _screenLoader = screenLoader;
            _gameplay = gameplay;
        }

        public void Resolve(string boardId)
        {
            _gameplay.Initialize(boardId);

            _screenLoader.Load("Gameplay", new GameplayViewData());
        }
    }
}