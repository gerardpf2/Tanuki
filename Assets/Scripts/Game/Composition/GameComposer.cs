using System.Collections.Generic;
using System.Linq;
using Game.Gameplay;
using Game.Gameplay.Composition;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Composition
{
    public class GameComposer : ScopeComposer
    {
        [NotNull] private readonly IGameplayDefinitionGetter _gameplayDefinitionGetter;
        [NotNull] private readonly IPieceViewDefinitionGetter _pieceViewDefinitionGetter;

        public GameComposer(
            [NotNull] IGameplayDefinitionGetter gameplayDefinitionGetter,
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter)
        {
            ArgumentNullException.ThrowIfNull(gameplayDefinitionGetter);
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            _gameplayDefinitionGetter = gameplayDefinitionGetter;
            _pieceViewDefinitionGetter = pieceViewDefinitionGetter;
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base
                .GetChildScopeComposers()
                .Append(new GameplayComposer(_gameplayDefinitionGetter, _pieceViewDefinitionGetter));
        }
    }
}