using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Phases.Phases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Composition
{
    public class PhasesComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new CameraTargetHighestPlayerPieceLockRowPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                PhasesComposerKeys.Phases.CameraTargetHighestPlayerPieceLockRowPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new DestroyNotAlivePiecesPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IGoals>()
                    )
                ),
                PhasesComposerKeys.Phases.DestroyNotAlivePiecesPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GoalsCompletedPhase(
                        r.Resolve<IGoals>()
                    )
                ),
                PhasesComposerKeys.Phases.GoalsCompletedPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GravityPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                PhasesComposerKeys.Phases.GravityPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiateInitialPiecesPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                PhasesComposerKeys.Phases.InstantiateInitialPiecesPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiatePlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                PhasesComposerKeys.Phases.InstantiatePlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LineClearPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                PhasesComposerKeys.Phases.LineClearPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LockPlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IMoves>()
                    )
                ),
                PhasesComposerKeys.Phases.LockPlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new NoMovesLeftPhase(
                        r.Resolve<IMoves>()
                    )
                ),
                PhasesComposerKeys.Phases.NoMovesLeftPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new SimulateInstantiatePlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>()
                    )
                ),
                PhasesComposerKeys.Phases.SimulateInstantiatePlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.InstantiateInitialPiecesPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.InstantiatePlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.CameraTargetHighestPlayerPieceLockRowPhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.Initial
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.CameraTargetHighestPlayerPieceLockRowPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.LockPlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.DestroyNotAlivePiecesPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.LineClearPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.GravityPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.GoalsCompletedPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.NoMovesLeftPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.SimulateInstantiatePlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.InstantiatePlayerPiecePhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.Lock
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phases.CameraTargetHighestPlayerPieceLockRowPhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.Move
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPhaseResolver>(_ => new PhaseResolver()));
        }
    }
}