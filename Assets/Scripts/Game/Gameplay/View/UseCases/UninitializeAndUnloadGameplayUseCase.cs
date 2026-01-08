using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.UseCases
{
    public class UninitializeAndUnloadGameplayUseCase : IUninitializeAndUnloadGameplayUseCase
    {
        [NotNull] private readonly IUninitializeGameplayUseCase _uninitializeGameplayUseCase;
        [NotNull] private readonly IUnloadGameplayUseCase _unloadGameplayUseCase;

        public UninitializeAndUnloadGameplayUseCase(
            [NotNull] IUninitializeGameplayUseCase uninitializeGameplayUseCase,
            [NotNull] IUnloadGameplayUseCase unloadGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(uninitializeGameplayUseCase);
            ArgumentNullException.ThrowIfNull(unloadGameplayUseCase);

            _uninitializeGameplayUseCase = uninitializeGameplayUseCase;
            _unloadGameplayUseCase = unloadGameplayUseCase;
        }

        public void Resolve()
        {
            _uninitializeGameplayUseCase.Resolve();
            _unloadGameplayUseCase.Resolve();
        }
    }
}