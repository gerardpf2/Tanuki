using Game.Gameplay.Board;
using Game.Gameplay.Phases;
using Game.Gameplay.Phases.Composition;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Player.Input;
using Game.Gameplay.View.Player.Input.ActionHandlers;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Player.Composition
{
    public class PlayerComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerInputActionHandler>(r =>
                    new LockPlayerInputActionHandler(
                        r.Resolve<IPhaseContainer>(PhasesComposerKeys.PhaseContainer.Lock),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>()
                    )
                ),
                "Lock"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerInputActionHandler>(r =>
                    new MoveLeftPlayerInputActionHandler(
                        r.Resolve<IPhaseContainer>(PhasesComposerKeys.PhaseContainer.Move),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>()
                    )
                ),
                "MoveLeft"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerInputActionHandler>(r =>
                    new MoveRightPlayerInputActionHandler(
                        r.Resolve<IPhaseContainer>(PhasesComposerKeys.PhaseContainer.Move),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>()
                    )
                ),
                "MoveRight"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerInputActionHandler>(r =>
                    new RotatePlayerInputActionHandler(
                        r.Resolve<IPhaseContainer>(PhasesComposerKeys.PhaseContainer.Move),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>()
                    )
                ),
                "Rotate"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerPieceGhostView>(r =>
                    new PlayerPieceGhostView(
                        r.Resolve<IBoard>("View"),
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IGameObjectPool>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerPieceView>(r =>
                    new PlayerPieceView(
                        r.Resolve<IBoard>("View"),
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IGameObjectPool>()
                    )
                )
            );
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<PlayerInputViewModel>((r, s) =>
                    s.Inject(
                        r.Resolve<IPlayerInputActionHandler>("Lock"),
                        r.Resolve<IPlayerInputActionHandler>("MoveLeft"),
                        r.Resolve<IPlayerInputActionHandler>("MoveRight"),
                        r.Resolve<IPlayerInputActionHandler>("Rotate")
                    )
                )
            );
        }
    }
}