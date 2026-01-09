using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Phases.Phases;
using Game.Gameplay.Pieces;
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
                        r.Resolve<IMoveCameraHelper>(),
                        r.Resolve<IEventEnqueuer>()
                    )
                ),
                PhasesComposerKeys.Phase.CameraTargetHighestPlayerPieceLockRowPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GoalsCompletedPhase(
                        r.Resolve<IGoals>()
                    )
                ),
                PhasesComposerKeys.Phase.GoalsCompletedPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new GravityPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>()
                    )
                ),
                PhasesComposerKeys.Phase.GravityPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiateInitialPiecesAndMoveCameraPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<IMoveCameraHelper>(),
                        r.Resolve<IEventEnqueuer>()
                    )
                ),
                PhasesComposerKeys.Phase.InstantiateInitialPiecesAndMoveCameraPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new InstantiatePlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>()
                    )
                ),
                PhasesComposerKeys.Phase.InstantiatePlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LineClearPhase(
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IDamagePieceHelper>()
                    )
                ),
                PhasesComposerKeys.Phase.LineClearPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LockPlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IMoves>()
                    )
                ),
                PhasesComposerKeys.Phase.LockPlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new NoMovesLeftPhase(
                        r.Resolve<IMoves>()
                    )
                ),
                PhasesComposerKeys.Phase.NoMovesLeftPhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new SimulateInstantiatePlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>()
                    )
                ),
                PhasesComposerKeys.Phase.SimulateInstantiatePlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new SwapCurrentNextPlayerPiecePhase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>()
                    )
                ),
                PhasesComposerKeys.Phase.SwapCurrentNextPlayerPiecePhase
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.SimulateInstantiatePlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.InstantiateInitialPiecesAndMoveCameraPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.InstantiatePlayerPiecePhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.Initial
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.CameraTargetHighestPlayerPieceLockRowPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.LockPlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.LineClearPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.GravityPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.GoalsCompletedPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.NoMovesLeftPhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.SimulateInstantiatePlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.InstantiatePlayerPiecePhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.Lock
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.CameraTargetHighestPlayerPieceLockRowPhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.Move
            );

            ruleAdder.Add(
                ruleFactory.GetTo<IPhaseContainer, IPhaseContainer>(
                    PhasesComposerKeys.PhaseContainer.Move
                ),
                PhasesComposerKeys.PhaseContainer.Rotate
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseContainer>(r =>
                    new PhaseContainer(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.SwapCurrentNextPlayerPiecePhase),
                        r.Resolve<IPhase>(PhasesComposerKeys.Phase.CameraTargetHighestPlayerPieceLockRowPhase)
                    )
                ),
                PhasesComposerKeys.PhaseContainer.SwapCurrentNext
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPhaseResolver>(_ => new PhaseResolver()));
        }
    }
}