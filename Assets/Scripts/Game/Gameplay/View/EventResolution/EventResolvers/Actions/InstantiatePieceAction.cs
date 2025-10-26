using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Pieces;
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

        protected override GameObject InstantiatePiece([NotNull] IPiece piece, [NotNull] IPieceViewDefinition pieceViewDefinition)
        {
            ArgumentNullException.ThrowIfNull(piece);
            ArgumentNullException.ThrowIfNull(pieceViewDefinition);

            _boardView.InstantiatePiece(piece, _sourceCoordinate, pieceViewDefinition.Prefab);

            return _boardView.GetPieceInstance(piece.Id);
        }
    }
}