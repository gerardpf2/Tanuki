using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.View.Actions.Composition;
using Game.Gameplay.View.Animation.Composition;
using Game.Gameplay.View.Board.Composition;
using Game.Gameplay.View.Camera.Composition;
using Game.Gameplay.View.EventResolvers.Composition;
using Game.Gameplay.View.Goals.Composition;
using Game.Gameplay.View.Moves.Composition;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Pieces.Composition;
using Game.Gameplay.View.Player.Composition;
using Infrastructure.DependencyInjection;
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