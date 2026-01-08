using Game.Gameplay.View.UseCases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.UseCases
{
    public class GoToMainMenuUseCase : IGoToMainMenuUseCase
    {
        [NotNull] private readonly IUnloadPauseMenuUseCase _unloadPauseMenuUseCase;
        [NotNull] private readonly IUninitializeAndUnloadGameplayUseCase _uninitializeAndUnloadGameplayUseCase;

        public GoToMainMenuUseCase(
            [NotNull] IUnloadPauseMenuUseCase unloadPauseMenuUseCase,
            [NotNull] IUninitializeAndUnloadGameplayUseCase uninitializeAndUnloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(unloadPauseMenuUseCase);
            ArgumentNullException.ThrowIfNull(uninitializeAndUnloadGameplayUseCase);

            _unloadPauseMenuUseCase = unloadPauseMenuUseCase;
            _uninitializeAndUnloadGameplayUseCase = uninitializeAndUnloadGameplayUseCase;
        }

        public void Resolve()
        {
            _unloadPauseMenuUseCase.Resolve();
            _uninitializeAndUnloadGameplayUseCase.Resolve();
        }
    }
}