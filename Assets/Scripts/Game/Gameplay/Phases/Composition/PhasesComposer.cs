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
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "CameraTargetHighestPlayerPieceLockRowPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new DestroyNotAlivePiecesPhase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IGoalsContainer>()
                    )
                ),
                "DestroyNotAlivePiecesPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GoalsCompletedPhase(
                        r.Resolve<IGoalsContainer>()
                    )
                ),
                "GoalsCompletedPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GravityPhase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "GravityPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiateInitialPiecesPhase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "InstantiateInitialPiecesPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiatePlayerPiecePhase(
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "InstantiatePlayerPiecePhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LineClearPhase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "LineClearPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LockPlayerPiecePhase(
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IMovesContainer>()
                    )
                ),
                "LockPlayerPiecePhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new NoMovesLeftPhase(
                        r.Resolve<IMovesContainer>()
                    )
                ),
                "NoMovesLeftPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new SimulateInstantiatePlayerPiecePhase(
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>()
                    )
                ),
                "SimulateInstantiatePlayerPiecePhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>("InstantiateInitialPiecesPhase"),
                        r.Resolve<IPhase>("InstantiatePlayerPiecePhase"),
                        r.Resolve<IPhase>("CameraTargetHighestPlayerPieceLockRowPhase")
                    )
                ),
                "Initial"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>("CameraTargetHighestPlayerPieceLockRowPhase"),
                        r.Resolve<IPhase>("LockPlayerPiecePhase"),
                        r.Resolve<IPhase>("DestroyNotAlivePiecesPhase"),
                        r.Resolve<IPhase>("LineClearPhase"),
                        r.Resolve<IPhase>("GravityPhase"),
                        r.Resolve<IPhase>("GoalsCompletedPhase"),
                        r.Resolve<IPhase>("NoMovesLeftPhase"),
                        r.Resolve<IPhase>("SimulateInstantiatePlayerPiecePhase"),
                        r.Resolve<IPhase>("InstantiatePlayerPiecePhase")
                    )
                ),
                "Lock"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>("CameraTargetHighestPlayerPieceLockRowPhase")
                    )
                ),
                "Move"
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPhaseResolver>(_ => new PhaseResolver()));
        }
    }
}