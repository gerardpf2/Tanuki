using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.UseCases
{
    public class ReloadGameplayUseCase : IReloadGameplayUseCase
    {
        [NotNull] private readonly IInitializeAndLoadAndRunGameplayUseCase _initializeAndLoadAndRunGameplayUseCase;
        [NotNull] private readonly IUninitializeGameplayUseCase _uninitializeGameplayUseCase;

        public ReloadGameplayUseCase(
            [NotNull] IInitializeAndLoadAndRunGameplayUseCase initializeAndLoadAndRunGameplayUseCase,
            [NotNull] IUninitializeGameplayUseCase uninitializeGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(initializeAndLoadAndRunGameplayUseCase); ;
            ArgumentNullException.ThrowIfNull(uninitializeGameplayUseCase);

            _initializeAndLoadAndRunGameplayUseCase = initializeAndLoadAndRunGameplayUseCase;
            _uninitializeGameplayUseCase = uninitializeGameplayUseCase;
        }

        public void Resolve(string id)
        {
            /*
             *
             * 1) Assumes that Gameplay is loaded
             * 2) Gameplay is not being unloaded
             * 3) The usage of IInitializeAndLoadAndRunGameplayUseCase instead of IInitializeGameplayUseCase is useful because
             *   3.1) It already handles both Initialize and Run
             *   3.2) Loading Gameplay has no effect (because it is already loaded, aside from focusing it which should not be necessary) but it also handles its SetData (and Run is based on a SetData callback)
             *
             */

            _uninitializeGameplayUseCase.Resolve();
            _initializeAndLoadAndRunGameplayUseCase.Resolve(id);
        }
    }
}