using System;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Camera;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Phases.Phases
{
    public abstract class BaseInstantiatePlayerPiecePhase : Phase
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly ICamera _camera;

        protected BaseInstantiatePlayerPiecePhase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoard board,
            [NotNull] ICamera camera)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(camera);

            _bagContainer = bagContainer;
            _board = board;
            _camera = camera;
        }

        protected override ResolveResult ResolveImpl(ResolveContext resolveContext)
        {
            IPiece piece = GetPiece();
            Coordinate sourceCoordinate = GetSourceCoordinate(piece);
            Coordinate lockSourceCoordinate = GetLockSourceCoordinate(piece, sourceCoordinate);

            return ResolveImpl(resolveContext, piece, sourceCoordinate, lockSourceCoordinate);
        }

        protected abstract ResolveResult ResolveImpl(
            ResolveContext resolveContext,
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate
        );

        [NotNull]
        private IPiece GetPiece()
        {
            IBag bag = _bagContainer.Bag;

            InvalidOperationException.ThrowIfNull(bag);

            return bag.Current;
        }

        private Coordinate GetSourceCoordinate([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            int row = Math.Max(_board.HighestNonEmptyRow + _camera.ExtraRowsOnTop, _camera.TopRow) - piece.Height + 1;
            int column = (_board.Columns - piece.Width + 1) / 2;

            return new Coordinate(row, column);
        }

        private Coordinate GetLockSourceCoordinate([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return _board.GetPieceLockSourceCoordinate(piece, sourceCoordinate);
        }
    }
}