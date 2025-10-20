using Game.Gameplay.Bag;
using Game.Gameplay.Bag.Parsing;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Camera;
using Game.Gameplay.Common;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.Moves;
using Game.Gameplay.Moves.Parsing;
using Game.Gameplay.Parsing;
using Game.Gameplay.PhaseResolution;
using Game.Gameplay.PhaseResolution.Phases;
using Game.Gameplay.REMOVE;
using Game.Gameplay.UseCases;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolution;
using Game.Gameplay.View.EventResolution.EventResolvers;
using Game.Gameplay.View.Header.Goals;
using Game.Gameplay.View.Header.Moves;
using Game.Gameplay.View.Input;
using Game.Gameplay.View.Player;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging;
using Infrastructure.ScreenLoading;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using Infrastructure.Tweening;
using Infrastructure.Tweening.BuilderHelpers;
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

            ruleAdder.Add(ruleFactory.GetSingleton<IBagPieceEntrySerializedDataConverter>(_ => new BagPieceEntrySerializedDataConverter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBagSerializedDataConverter>(r =>
                    new BagSerializedDataConverter(
                        r.Resolve<IBagPieceEntrySerializedDataConverter>(),
                        r.Resolve<IPieceGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IBagContainer>(_ => new BagContainer()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardSerializedDataConverter>(r =>
                    new BoardSerializedDataConverter(
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

            ruleAdder.Add(ruleFactory.GetSingleton<IWorldPositionGetter>(_ => new WorldPositionGetter()));

            ruleAdder.Add(ruleFactory.GetSingleton<IEventEnqueuer>(_ => new EventEnqueuer()));

            ruleAdder.Add(ruleFactory.GetSingleton<IEventFactory>(_ => new EventFactory()));

            ruleAdder.Add(ruleFactory.GetSingleton<IGoalSerializedDataConverter>(_ => new GoalSerializedDataConverter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGoalsSerializedDataConverter>(r =>
                    new GoalsSerializedDataConverter(
                        r.Resolve<IGoalSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IGoalsContainer>(_ => new GoalsContainer()));

            ruleAdder.Add(ruleFactory.GetSingleton<IMovesSerializedDataConverter>(_ => new MovesSerializedDataConverter()));

            ruleAdder.Add(ruleFactory.GetSingleton<IMovesContainer>(_ => new MovesContainer()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGameplayParser>(r =>
                    new GameplayParser(
                        r.Resolve<IGameplaySerializedDataConverter>(),
                        r.Resolve<IParser>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IGameplaySerializedDataConverter>(r =>
                    new GameplaySerializedDataConverter(
                        r.Resolve<IBoardSerializedDataConverter>(),
                        r.Resolve<IGoalsSerializedDataConverter>(),
                        r.Resolve<IMovesSerializedDataConverter>(),
                        r.Resolve<IBagSerializedDataConverter>()
                    )
                )
            );

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
                        r.Resolve<IBagContainer>(),
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
                        r.Resolve<IPhase>("NoMovesLeftPhase"),
                        r.Resolve<IPhase>("InstantiatePlayerPiecePhase")
                    )
                )
            );

            // Not shared so it can only be unloaded from here
            ruleAdder.Add(
                ruleFactory.GetSingleton<IUnloadGameplayUseCase>(r =>
                    new UnloadGameplayUseCase(
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IMovesContainer>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IInputHandler>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IMovementFactory>(r =>
                    new MovementFactory(
                        r.Resolve<ITransformTweenBuilderHelper>(),
                        r.Resolve<ITweenRunner>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IMovementHelper>(r =>
                    new MovementHelper(
                        r.Resolve<IMovementFactory>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardView>(r =>
                    new BoardView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IWorldPositionGetter>(),
                        r.Resolve<ILogger>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetInstance(_pieceViewDefinitionGetter));

            ruleAdder.Add(
                ruleFactory.GetSingleton<ICameraView>(r =>
                    new CameraView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IWorldPositionGetter>(),
                        r.Resolve<ICameraGetter>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IActionFactory>(r =>
                    new ActionFactory(
                        r.Resolve<IMovementHelper>(),
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<ICoroutineRunner>()
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
                ruleFactory.GetSingleton<IMovesView>(r =>
                    new MovesView(
                        r.Resolve<IMovesContainer>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetSingleton<IInputHandler>(r =>
                    new InputHandler(
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IInputListener>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IScreenPropertiesGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton(_ => new InputEventsHandler()));
            ruleAdder.Add(ruleFactory.GetTo<IInputListener, InputEventsHandler>());
            ruleAdder.Add(ruleFactory.GetTo<IInputNotifier, InputEventsHandler>());

            ruleAdder.Add(
                ruleFactory.GetSingleton<IPlayerPieceView>(r =>
                    new PlayerPieceView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IWorldPositionGetter>()
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
                        r.Resolve<IGameplayDefinitionGetter>(),
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IMovesContainer>(),
                        r.Resolve<IGameplayParser>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IInputHandler>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<BoardViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<ICameraView>(),
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
                ruleFactory.GetInject<MovesViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IMovesView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<InputCatcher>((r, s) =>
                    s.Inject(
                        r.Resolve<IInputNotifier>()
                    )
                )
            );
        }
    }
}