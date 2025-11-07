using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Phases;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
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
                ruleFactory.GetSingleton<IPlayerPieceGhostView>(r =>
                    new PlayerPieceGhostView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IGameObjectPool>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerPieceView>(r =>
                    new PlayerPieceView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
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
                        r.Resolve<IPhaseContainer>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<PlayerPieceInputHandler>((r, s) =>
                    s.Inject(
                        r.Resolve<IPhaseContainer>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IScreenPropertiesGetter>()
                    )
                )
            );
        }
    }
}