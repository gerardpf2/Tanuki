using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Camera;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.PhaseResolution.Phases;
using Game.Gameplay.Player;
using Game.Gameplay.REMOVE;
using Game.Gameplay.UseCases;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Player;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.Composition
{
    public class GameplayComposer : ScopeComposer
    {
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;

        public GameplayComposer(
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardParser>(r =>
                    new BoardParser(
                        r.Resolve<IBoardSerializedDataConverter>(),
                        r.Resolve<IParser>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardSerializedDataConverter>(r =>
                    new BoardSerializedDataConverter(
                        r.Resolve<IPieceCachedPropertiesGetter>(),
                        r.Resolve<IPiecePlacementSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPiecePlacementSerializedDataConverter>(r =>
                    new PiecePlacementSerializedDataConverter(
                        r.Resolve<IPieceSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceSerializedDataConverter>(r =>
                    new PieceSerializedDataConverter(
                        r.Resolve<IPieceGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IBoardContainer>(_ => new BoardContainer()));

            ruleAdder.Add(ruleFactory.GetSingleton<IPieceCachedPropertiesGetter>(_ => new PieceCachedPropertiesGetter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceFactory>(r =>
                    new PieceFactory(
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<IConverter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPieceGetter>(r =>
                    new PieceGetter(
                        r.Resolve<IPieceFactory>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IPieceIdGetter>(_ => new PieceIdGetter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<ICamera>(r =>
                    new Camera.Camera(
                        r.Resolve<IBoardContainer>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IEventEnqueuer>(_ => new EventEnqueuer()));

            ruleAdder.Add(ruleFactory.GetSingleton<IEventFactory>(_ => new EventFactory()));

            ruleAdder.Add(ruleFactory.GetSingleton<IGoalSerializedDataConverter>(_ => new GoalSerializedDataConverter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoalsParser>(r =>
                    new GoalsParser(
                        r.Resolve<IGoalsSerializedDataConverter>(),
                        r.Resolve<IParser>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoalsSerializedDataConverter>(r =>
                    new GoalsSerializedDataConverter(
                        r.Resolve<IGoalSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IGoalsContainer>(_ => new GoalsContainer()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new CameraTargetTopRowPhase(
                        r.Resolve<ICamera>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "CameraTargetTopRowPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new DestroyNotAlivePiecesPhase(
                        r.Resolve<IBoardContainer>(),
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
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IPlayerPiecesBag>()
                    )
                ),
                "InstantiatePlayerPiecePhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LineClearPhase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>()
                    )
                ),
                "LineClearPhase"
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhase>(r =>
                    new LockPlayerPiecePhase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IEventFactory>(),
                        r.Resolve<IPlayerPiecesBag>()
                    )
                ),
                "LockPlayerPiecePhase"
            );

            // TODO: Review phases order
            ruleAdder.Add(
                ruleFactory.GetSingleton<IPhaseResolver>(r =>
                    new PhaseResolver(
                        r.Resolve<IPhase>("GoalsCompletedPhase"),
                        r.Resolve<IPhase>("InstantiateInitialPiecesPhase"),
                        r.Resolve<IPhase>("CameraTargetTopRowPhase"),
                        r.Resolve<IPhase>("LockPlayerPiecePhase"),
                        r.Resolve<IPhase>("DestroyNotAlivePiecesPhase"),
                        r.Resolve<IPhase>("GravityPhase"),
                        r.Resolve<IPhase>("LineClearPhase"),
                        r.Resolve<IPhase>("InstantiatePlayerPiecePhase")
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerPiecesBag>(r =>
                    new PlayerPiecesBag(
                        r.Resolve<IPieceGetter>()
                    )
                )
            );

            // Not shared so it can only be unloaded from here
            ruleAdder.Add(
                ruleFactory.GetSingleton<IUnloadGameplayUseCase>(r =>
                    new UnloadGameplayUseCase(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPlayerPiecesBag>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IPlayerView>(),
                        r.Resolve<ICameraController>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardView>(r =>
                    new BoardView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceCachedPropertiesGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetInstance(_pieceViewDefinitionGetter));

            ruleAdder.Add(
                ruleFactory.GetSingleton<ICameraView>(r =>
                    new CameraView(
                        r.Resolve<ICamera>(),
                        r.Resolve<ICameraGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<ICameraController>(r =>
                    new CameraController(
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IActionFactory>(r =>
                    new ActionFactory(
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IPlayerView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventResolverFactory>(r =>
                    new EventResolverFactory(
                        r.Resolve<IActionFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventsResolver>(r =>
                    new EventsResolver(
                        r.Resolve<IEventEnqueuer>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IEventsResolverSingle>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IEventsResolverSingle>(r =>
                    new EventsResolverSingle(
                        r.Resolve<IEventResolverFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoalsView>(r =>
                    new GoalsView(
                        r.Resolve<IGoalsContainer>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerView>(r =>
                    new PlayerView(
                        r.Resolve<IPieceCachedPropertiesGetter>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraController>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetInstance(_gameplayDefinitionGetter));
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<LoadUnloadGameplay>((r, s) =>
                    s.Inject(
                        r.Resolve<ILoadGameplayUseCase>(),
                        r.Resolve<IUnloadGameplayUseCase>()
                    )
                )
            );

            // Shared so it can be loaded from anywhere
            ruleAdder.Add(
                ruleFactory.GetSingleton<ILoadGameplayUseCase>(r =>
                    new LoadGameplayUseCase(
                        r.Resolve<IUnloadGameplayUseCase>(),
                        r.Resolve<IBoardParser>(),
                        r.Resolve<IGameplayDefinitionGetter>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<IGoalsParser>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IPlayerPiecesBag>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IPlayerView>(),
                        r.Resolve<ICameraController>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<BoardViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<ICameraView>(),
                        r.Resolve<ICameraController>(),
                        r.Resolve<ICoroutineRunnerHelper>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<GoalsViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IGoalsView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<PlayerInputHandler>((r, s) =>
                    s.Inject(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IPlayerView>(),
                        r.Resolve<IScreenPropertiesGetter>()
                    )
                )
            );
        }
    }
}