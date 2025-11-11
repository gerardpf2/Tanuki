using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Bag;
using Game.Gameplay.Bag.Composition;
using Game.Gameplay.Bag.Parsing;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Composition;
using Game.Gameplay.Board.Parsing;
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
using Game.Gameplay.Pieces.Composition;
using Game.Gameplay.REMOVE;
using Game.Gameplay.View.Composition;
using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging;
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
                        r.Resolve<IBag>(),
                        r.Resolve<IBoard>(),
                        r.Resolve<IGoals>(),
                        r.Resolve<IMoves>(),
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
                ruleFactory.GetSingleton<IGameplaySerializerOnBeginIteration>(r =>
                    new GameplaySerializerOnBeginIteration(
                        r.Resolve<IGameplayParser>(),
                        r.Resolve<IPhaseResolver>(),
                        r.Resolve<ILogger>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetInstance(_gameplayDefinitionGetter));
        }

        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base
                .GetPartialScopeComposers()
                .Append(new BagComposer())
                .Append(new BoardComposer())
                .Append(new CameraComposer())
                .Append(new EventsComposer())
                .Append(new GoalsComposer())
                .Append(new MovesComposer())
                .Append(new PhasesComposer())
                .Append(new PiecesComposer());
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new GameplayViewComposer(_pieceViewDefinitionGetter));
        }
    }
}