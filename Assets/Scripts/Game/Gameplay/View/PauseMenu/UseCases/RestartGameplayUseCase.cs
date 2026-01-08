using Game.Gameplay.View.UseCases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.UseCases
{
    public class RestartGameplayUseCase : IRestartGameplayUseCase
    {
        [NotNull] private readonly IUnloadPauseMenuUseCase _unloadPauseMenuUseCase;
        [NotNull] private readonly IReloadGameplayUseCase _reloadGameplayUseCase;

        public RestartGameplayUseCase(
            [NotNull] IUnloadPauseMenuUseCase unloadPauseMenuUseCase,
            [NotNull] IReloadGameplayUseCase reloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(unloadPauseMenuUseCase);
            ArgumentNullException.ThrowIfNull(reloadGameplayUseCase);

            _unloadPauseMenuUseCase = unloadPauseMenuUseCase;
            _reloadGameplayUseCase = reloadGameplayUseCase;
        }

        public void Resolve(string id)
        {
            _unloadPauseMenuUseCase.Resolve();
            _reloadGameplayUseCase.Resolve(id);
        }
    }
}