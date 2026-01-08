using System;
using Infrastructure.ScreenLoading;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Game.Gameplay.View.UseCases
{
    public class LoadGameplayUseCase : ILoadGameplayUseCase
    {
        [NotNull] private readonly IScreenLoader _screenLoader;

        public LoadGameplayUseCase([NotNull] IScreenLoader screenLoader)
        {
            ArgumentNullException.ThrowIfNull(screenLoader);

            _screenLoader = screenLoader;
        }

        public void Resolve(Action onReady)
        {
            GameplayViewData gameplayViewData = new(onReady);

            _screenLoader.Load(GameplayConstants.ScreenKey, gameplayViewData);
        }
    }
}