using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.PauseMenu.UseCases
{
    public class ResumeGameplayUseCase : IResumeGameplayUseCase
    {
        [NotNull] private readonly IUnloadPauseMenuUseCase _unloadPauseMenuUseCase;

        public ResumeGameplayUseCase([NotNull] IUnloadPauseMenuUseCase unloadPauseMenuUseCase)
        {
            ArgumentNullException.ThrowIfNull(unloadPauseMenuUseCase);

            _unloadPauseMenuUseCase = unloadPauseMenuUseCase;
        }

        public void Resolve()
        {
            _unloadPauseMenuUseCase.Resolve();
        }
    }
}