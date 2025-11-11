using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Parsing;
using Game.Gameplay.Phases;
using Game.Gameplay.Pieces;
using Game.Gameplay.REMOVE;
using Game.Gameplay.View.Actions.Composition;
using Game.Gameplay.View.Animation.Composition;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Board.Composition;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.Camera.Composition;
using Game.Gameplay.View.EventResolvers;
using Game.Gameplay.View.EventResolvers.Composition;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Goals.Composition;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Moves.Composition;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Pieces.Composition;
using Game.Gameplay.View.Player;
using Game.Gameplay.View.Player.Composition;
using Game.Gameplay.View.REMOVE;
using Game.Gameplay.View.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.ScreenLoading;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Composition
{
    public class GameplayViewComposer : ScopeComposer
    {
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;

        public GameplayViewComposer([NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            // Not shared so it can only be unloaded from here
            ruleAdder.Add(
                ruleFactory.GetSingleton<IUnloadGameplayUseCase>(r =>
                    new UnloadGameplayUseCase(
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IGoals>(),
                        r.Resolve<IMoves>(),
                        r.Resolve<IGameplaySerializerOnBeginIteration>(),
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
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

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
                        r.Resolve<IPieceIdGetter>(),
                        r.Resolve<ICamera>(),
                        r.Resolve<IGameplayParser>(),
                        r.Resolve<IPhaseContainer>("Initial"),
                        r.Resolve<IGameplaySerializerOnBeginIteration>(),
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
                .Append(new ActionsComposer())
                .Append(new AnimationComposer())
                .Append(new BoardComposer())
                .Append(new CameraComposer())
                .Append(new EventResolversComposer())
                .Append(new GoalsComposer())
                .Append(new MovesComposer())
                .Append(new PiecesComposer(_pieceViewDefinitionGetter))
                .Append(new PlayerComposer());
        }
    }
}