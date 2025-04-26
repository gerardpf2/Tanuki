using System.Collections.Generic;
using Game.Gameplay.Model.Board;
using Game.Gameplay.Model.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Model.PhaseResolution.Phases
{
    public class InitializePhase : IInitializePhase
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;

        public InitializePhase([NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _pieceGetter = pieceGetter;
        }

        public void Resolve([NotNull] IBoard board, [NotNull, ItemNotNull] IEnumerable<PiecePlacement> piecePlacements)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piecePlacements);

            foreach (PiecePlacement piecePlacement in piecePlacements)
            {
                ArgumentNullException.ThrowIfNull(piecePlacement);

                IPiece piece = _pieceGetter.Get(piecePlacement.PieceType);
                Coordinate sourceCoordinate = new(piecePlacement.Row, piecePlacement.Column);

                board.Add(piece, sourceCoordinate);

                // TODO: Add action
            }
        }
    }
}