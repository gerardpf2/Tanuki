using Game.Gameplay.Board;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.PhaseResolution.Phases;
using Game.Gameplay.Player;
using Game.Gameplay.UseCases;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.Player;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.Composition
{
    public class GameplayComposer : ScopeComposer
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;

        public GameplayComposer(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            _boardDefinitionGetter = boardDefinitionGetter;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
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
            ruleAdder.Add(ruleFactory.GetTo<IEventDequeuer, EventContainer>());
            ruleAdder.Add(ruleFactory.GetTo<IEventEnqueuer, EventContainer>());

            ruleAdder.Add(ruleFactory.GetSingleton<IEventFactory>(_ => new EventFactory()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IInstantiateInitialPiecesPhase>(r =>
                    new InstantiateInitialPiecesPhase(
                        r.Resolve<IPieceGetter>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IInstantiatePlayerPiecePhase>(r =>
                    new InstantiatePlayerPiecePhase(
                        r.Resolve<IPieceGetter>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IPlayerPiecesBag>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseResolver>(r =>
                    new PhaseResolver(
                        r.Resolve<IInstantiateInitialPiecesPhase>(),
                        r.Resolve<IInstantiatePlayerPiecePhase>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPlayerPiecesBag>(_ => new PlayerPiecesBag()));

            ruleAdder.Add(ruleFactory.GetSingleton<IBoardView>(_ => new BoardView()));

            ruleAdder.Add(ruleFactory.GetInstance(_pieceViewDefinitionGetter));

            ruleAdder.Add(
                ruleFactory.GetSingleton(r =>
                    new CameraController(
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraGetter>()
                    )
                )
            );
            ruleAdder.Add(ruleFactory.GetTo<ICameraController, CameraController>());
            ruleAdder.Add(ruleFactory.GetTo<ICameraBoardViewPropertiesGetter, CameraController>());
            ruleAdder.Add(ruleFactory.GetTo<ICameraBoardViewPropertiesSetter, CameraController>());

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventListener>(r =>
                    new EventListener(
                        r.Resolve<IEventDequeuer>(),
                        r.Resolve<IEventsResolver>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventResolverFactory>(r =>
                    new EventResolverFactory(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<IPlayerView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventsResolver>(r =>
                    new EventsResolver(
                        r.Resolve<IEventResolverFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerView>(r =>
                    new PlayerView(
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraBoardViewPropertiesGetter>()
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
                        r.Resolve<IBoardDefinitionGetter>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPlayerPiecesBag>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<IPlayerView>(),
                        r.Resolve<ICameraController>(),
                        r.Resolve<IEventListener>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<BoardViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<ICameraBoardViewPropertiesSetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<PlayerInputHandler>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IPlayerView>(),
                        r.Resolve<IScreenPropertiesGetter>()
                    )
                )
            );
        }
    }
}