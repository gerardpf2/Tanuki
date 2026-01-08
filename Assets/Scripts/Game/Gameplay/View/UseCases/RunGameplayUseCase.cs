using Game.Gameplay.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.UseCases
{
    public class RunGameplayUseCase : IRunGameplayUseCase
    {
        [NotNull] private readonly IPhaseContainer _phaseContainerInitial;

        public RunGameplayUseCase([NotNull] IPhaseContainer phaseContainerInitial)
        {
            ArgumentNullException.ThrowIfNull(phaseContainerInitial);

            _phaseContainerInitial = phaseContainerInitial;
        }

        public void Resolve()
        {
            _phaseContainerInitial.Resolve(new ResolveContext(ResolveReason.Load, null, null));
        }
    }
}