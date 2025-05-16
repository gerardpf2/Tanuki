using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.EventResolution.EventResolvers.Actions
{
    public class InstantiatePieceAction : BaseInstantiatePieceAction
    {
        [NotNull] private readonly IBoardView _boardView;
        private readonly Coordinate _sourceCoordinate;

        public InstantiatePieceAction(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IBoardView boardView,
            Coordinate sourceCoordinate) : base(pieceViewDefinitionGetter, piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _boardView = boardView;
            _sourceCoordinate = sourceCoordinate;
        }

        protected override GameObject Instantiate(IPiece piece, [NotNull] IPieceViewDefinition pieceViewDefinition)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinition);

            return _boardView.Instantiate(piece, _sourceCoordinate, pieceViewDefinition.Prefab);
        }
    }
}