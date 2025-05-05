using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class InstantiatePlayerPiecePhase : IInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        public InstantiatePlayerPiecePhase(
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

        public void Resolve()
        {
            const PieceType pieceType = PieceType.PlayerBlock;

            IPiece piece = _pieceGetter.Get(pieceType);

            // TODO: Player controller, etc ¿?

            _eventEnqueuer.Enqueue(_eventFactory.GetInstantiatePlayerPieceEvent(piece, pieceType));
        }
    }
}