using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Bag;
using Game.Gameplay.Bag.Composition;
using Game.Gameplay.Bag.Parsing;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Composition;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Camera;
using Game.Gameplay.Camera.Composition;
using Game.Gameplay.Events.Composition;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Composition;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.Moves;
using Game.Gameplay.Moves.Composition;
using Game.Gameplay.Moves.Parsing;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Game.Gameplay.Phases.Composition;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Composition;
using Game.Gameplay.REMOVE;
using Game.Gameplay.UseCases;
using Game.Gameplay.View.Actions.Composition;
using Game.Gameplay.View.Animation.Composition;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.EventResolvers.Composition;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Player;
using Game.Gameplay.View.Player.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
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

            // Not shared so it can only be unloaded from here
            ruleAdder.Add(
                ruleFactory.GetSingleton<IUnloadGameplayUseCase>(r =>
                    new UnloadGameplayUseCase(
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IMovesContainer>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
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
                ruleFactory.GetInject<GameplaySerialize>((r, s) =>
                    s.Inject(
                        r.Resolve<IBagContainer>(),
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IMovesContainer>(),
                        r.Resolve<IGameplayParser>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<UnloadGameplay>((r, s) =>
                    s.Inject(
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
                        r.Resolve<ICamera>(),
                        r.Resolve<IGoalsContainer>(),
                        r.Resolve<IMovesContainer>(),
                        r.Resolve<IGameplayParser>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IPieceGameObjectPreloader>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<IEventsResolver>(),
                        r.Resolve<IScreenLoader>()
                    )
                )
            );
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base
                .GetPartialScopeComposers()
                // Model
                .Append(new BagComposer())
                .Append(new BoardComposer())
                .Append(new CameraComposer())
                .Append(new EventsComposer())
                .Append(new GoalsComposer())
                .Append(new MovesComposer())
                .Append(new PhasesComposer())
                .Append(new PiecesComposer())
                // View
                .Append(new ActionsComposer())
                .Append(new AnimationComposer())
                .Append(new View.Board.Composition.BoardComposer())
                .Append(new View.Camera.Composition.CameraComposer())
                .Append(new EventResolversComposer())
                .Append(new View.Goals.Composition.GoalsComposer())
                .Append(new View.Moves.Composition.MovesComposer())
                .Append(new View.Pieces.Composition.PiecesComposer(_pieceViewDefinitionGetter))
                .Append(new PlayerComposer());
        }
    }
}