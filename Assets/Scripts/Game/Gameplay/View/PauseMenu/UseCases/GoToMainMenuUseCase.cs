using Game.Gameplay.View.UseCases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.UseCases
{
    public class GoToMainMenuUseCase : IGoToMainMenuUseCase
    {
        [NotNull] private readonly IUnloadPauseMenuUseCase _unloadPauseMenuUseCase;
        [NotNull] private readonly IUnloadGameplayUseCase _unloadGameplayUseCase;

        public GoToMainMenuUseCase(
            [NotNull] IUnloadPauseMenuUseCase unloadPauseMenuUseCase,
            [NotNull] IUnloadGameplayUseCase unloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(unloadPauseMenuUseCase);
            ArgumentNullException.ThrowIfNull(unloadGameplayUseCase);

            _unloadPauseMenuUseCase = unloadPauseMenuUseCase;
            _unloadGameplayUseCase = unloadGameplayUseCase;
        }

        public void Resolve()
        {
            _unloadPauseMenuUseCase.Resolve();
            _unloadGameplayUseCase.Resolve();
        }
    }
}