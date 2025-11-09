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
    public class SimulateInstantiatePlayerPiecePhase : Phase
    {
        [NotNull] private readonly IBagContainer _bagContainer;
        [NotNull] private readonly IBoardContainer _boardContainer;
        [NotNull] private readonly ICamera _camera;

        public SimulateInstantiatePlayerPiecePhase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoardContainer boardContainer,
            [NotNull] ICamera camera)
        {
            ArgumentNullException.ThrowIfNull(bagContainer);
            ArgumentNullException.ThrowIfNull(boardContainer);
            ArgumentNullException.ThrowIfNull(camera);

            _bagContainer = bagContainer;
            _boardContainer = boardContainer;
            _camera = camera;
        }

        protected override ResolveResult ResolveImpl([NotNull] ResolveContext resolveContext)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (!resolveContext.PieceSourceCoordinate.HasValue || !resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                return ResolveResult.NotUpdated;
            }

            Coordinate prevSourceCoordinate = resolveContext.PieceSourceCoordinate.Value;
            Coordinate prevLockSourceCoordinate = resolveContext.PieceLockSourceCoordinate.Value;

            IPiece piece = GetPiece();
            Coordinate newSourceCoordinate = GetSourceCoordinate(piece);
            Coordinate newLockSourceCoordinate = GetLockSourceCoordinate(piece, newSourceCoordinate);

            if (newSourceCoordinate.Equals(prevSourceCoordinate) &&
                newLockSourceCoordinate.Equals(prevLockSourceCoordinate))
            {
                return ResolveResult.NotUpdated;
            }

            resolveContext.SetPieceSourceCoordinate(newSourceCoordinate, newLockSourceCoordinate);

            return ResolveResult.Updated;
        }

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

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            int row = Math.Max(board.HighestNonEmptyRow + _camera.ExtraRowsOnTop, _camera.TopRow) - piece.Height + 1;
            int column = (board.Columns - piece.Width + 1) / 2;

            return new Coordinate(row, column);
        }

        private Coordinate GetLockSourceCoordinate([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IBoard board = _boardContainer.Board;

            InvalidOperationException.ThrowIfNull(board);

            return board.GetPieceLockSourceCoordinate(piece, sourceCoordinate);
        }
    }
}