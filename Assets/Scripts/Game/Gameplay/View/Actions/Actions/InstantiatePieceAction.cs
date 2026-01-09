using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Board;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Actions.Actions
{
    public class InstantiatePieceAction : BaseInstantiatePieceAction
    {
        [NotNull] private readonly IBoardView _boardView;
        private readonly Coordinate _sourceCoordinate;

        public InstantiatePieceAction(
            [NotNull] IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            [NotNull] IBoardView boardView,
            Coordinate sourceCoordinate) : base(piece, instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(boardView);

            _boardView = boardView;
            _sourceCoordinate = sourceCoordinate;
        }

        protected override GameObject InstantiatePiece([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            _boardView.InstantiatePiece(piece, _sourceCoordinate);

            return _boardView.GetPieceInstance(piece.Id);
        }
    }
}