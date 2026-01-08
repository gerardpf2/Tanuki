using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.UseCases
{
    public class InitializeAndLoadAndRunGameplayUseCase : IInitializeAndLoadAndRunGameplayUseCase
    {
        [NotNull] private readonly IInitializeGameplayUseCase _initializeGameplayUseCase;
        [NotNull] private readonly ILoadGameplayUseCase _loadGameplayUseCase;
        [NotNull] private readonly IRunGameplayUseCase _runGameplayUseCase;

        public InitializeAndLoadAndRunGameplayUseCase(
            [NotNull] IInitializeGameplayUseCase initializeGameplayUseCase,
            [NotNull] ILoadGameplayUseCase loadGameplayUseCase,
            [NotNull] IRunGameplayUseCase runGameplayUseCase)
        {
            ArgumentNullException.ThrowIfNull(initializeGameplayUseCase);
            ArgumentNullException.ThrowIfNull(loadGameplayUseCase);
            ArgumentNullException.ThrowIfNull(runGameplayUseCase);

            _initializeGameplayUseCase = initializeGameplayUseCase;
            _loadGameplayUseCase = loadGameplayUseCase;
            _runGameplayUseCase = runGameplayUseCase;
        }

        public void Resolve(string id)
        {
            _initializeGameplayUseCase.Resolve(id);
            _loadGameplayUseCase.Resolve(_runGameplayUseCase.Resolve);
        }
    }
}