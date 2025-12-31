using Game.Gameplay.Phases;
using Game.Gameplay.Phases.Composition;
using Game.Gameplay.Phases.Phases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.GameplayEditor.Phases.Composition
{
    public class PhasesEditorComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.SimulateInstantiatePlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.InstantiateInitialPiecesAndMoveCameraPhase)
                    )
                ),
                PhasesEditorComposerKeys.PhaseContainer.InitialEditor
            );
        }
    }
}