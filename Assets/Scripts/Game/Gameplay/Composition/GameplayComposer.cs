using Game.Gameplay.Board;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.PhaseResolution.Phases;
using Game.Gameplay.UseCases;
using Game.Gameplay.View.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Composition
{
    public class GameplayComposer : ScopeComposer
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;

        public GameplayComposer([NotNull] IBoardDefinitionGetter boardDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);

            _boardDefinitionGetter = boardDefinitionGetter;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetInstance(_boardDefinitionGetter));

            ruleAdder.Add(ruleFactory.GetSingleton<IPieceFactory>(_ => new PieceFactory()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGetter>(r =>
                    new PieceGetter(
                        r.Resolve<IPieceFactory>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new EventContainer()));
            ruleAdder.Add(ruleFactory.GetTo<IEventEnqueuer, EventContainer>());
            ruleAdder.Add(ruleFactory.GetTo<IEventDequeuer, EventContainer>());

            ruleAdder.Add(ruleFactory.GetSingleton<IEventFactory>(_ => new EventFactory()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IInitializePhase>(r =>
                    new InitializePhase(
                        r.Resolve<IPieceGetter>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseResolver>(r =>
                    new PhaseResolver(
                        r.Resolve<IInitializePhase>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IBoardView>(_ => new BoardView()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGameplay>(r =>
                    new Gameplay(
                        r.Resolve<IBoardDefinitionGetter>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IBoardView>()
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
                ruleFactory.GetSingleton<ILoadGameplay>(r =>
                    new LoadGameplay(
                        r.Resolve<IScreenLoader>(),
                        r.Resolve<IGameplay>()
                    )
                )
            );
        }
    }
}