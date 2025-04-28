using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class InitializePhase : IInitializePhase
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public InitializePhase(
            [NotNull] IPieceGetter pieceGetter,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _pieceGetter = pieceGetter;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        public void Resolve([NotNull] IBoard board, [NotNull, ItemNotNull] IEnumerable<IPiecePlacement> piecePlacements)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(piecePlacements);

            foreach (IPiecePlacement piecePlacement in piecePlacements)
            {
                ArgumentNullException.ThrowIfNull(piecePlacement);

                IPiece piece = _pieceGetter.Get(piecePlacement.PieceType);
                Coordinate sourceCoordinate = new(piecePlacement.Row, piecePlacement.Column);

                board.Add(piece, sourceCoordinate);

                _eventEnqueuer.Enqueue(
                    _eventFactory.GetInstantiate(
                        piece,
                        piecePlacement.PieceType,
                        sourceCoordinate
                    )
                );
            }
        }
    }
}