using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Board;
using Game.Gameplay.Composition;
using Game.Gameplay.UseCases;
using Game.Gameplay.View.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Composition
{
    public class GameComposer : ScopeComposer
    {
        [NotNull] private readonly IBoardDefinitionGetter _boardDefinitionGetter;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;

        public GameComposer(
            [NotNull] IBoardDefinitionGetter boardDefinitionGetter,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(boardDefinitionGetter);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            _boardDefinitionGetter = boardDefinitionGetter;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base
                .GetChildScopeComposers()
                .Append(new GameplayComposer(_boardDefinitionGetter, _pieceViewDefinitionGetter));
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            base.Initialize(ruleResolver);

            ruleResolver.Resolve<ILoadGameplay>().Resolve("Test"); // TODO
        }
    }
}