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
                nameof(CameraTargetHighestPlayerPieceLockRowPhase)
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
                nameof(DestroyNotAlivePiecesPhase)
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GoalsCompletedPhase(
                        r.Resolve<IGoals>()
                    )
                ),
                nameof(GoalsCompletedPhase)
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GravityPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                nameof(GravityPhase)
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiateInitialPiecesPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                nameof(InstantiateInitialPiecesPhase)
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
                nameof(InstantiatePlayerPiecePhase)
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
                nameof(LineClearPhase)
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
                nameof(LockPlayerPiecePhase)
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new NoMovesLeftPhase(
                        r.Resolve<IMoves>()
                    )
                ),
                nameof(NoMovesLeftPhase)
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new SimulateInstantiatePlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>()
                    )
                ),
                nameof(SimulateInstantiatePlayerPiecePhase)
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(nameof(InstantiateInitialPiecesPhase)),
                        r.Resolve<IPhase>(nameof(InstantiatePlayerPiecePhase)),
                        r.Resolve<IPhase>(nameof(CameraTargetHighestPlayerPieceLockRowPhase))
                    )
                ),
                "Initial"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(nameof(CameraTargetHighestPlayerPieceLockRowPhase)),
                        r.Resolve<IPhase>(nameof(LockPlayerPiecePhase)),
                        r.Resolve<IPhase>(nameof(DestroyNotAlivePiecesPhase)),
                        r.Resolve<IPhase>(nameof(LineClearPhase)),
                        r.Resolve<IPhase>(nameof(GravityPhase)),
                        r.Resolve<IPhase>(nameof(GoalsCompletedPhase)),
                        r.Resolve<IPhase>(nameof(NoMovesLeftPhase)),
                        r.Resolve<IPhase>(nameof(SimulateInstantiatePlayerPiecePhase)),
                        r.Resolve<IPhase>(nameof(InstantiatePlayerPiecePhase))
                    )
                ),
                "Lock"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(nameof(CameraTargetHighestPlayerPieceLockRowPhase))
                    )
                ),
                "Move"
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPhaseResolver>(_ => new PhaseResolver()));
        }
    }
}