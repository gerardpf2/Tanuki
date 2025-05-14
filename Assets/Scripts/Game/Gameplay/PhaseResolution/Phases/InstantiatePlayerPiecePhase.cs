using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class InstantiatePlayerPiecePhase : IInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IPieceGetter _pieceGetter;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        public InstantiatePlayerPiecePhase(
            [NotNull] IPieceGetter pieceGetter,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IPlayerPiecesBag playerPiecesBag)
        {
            ArgumentNullException.ThrowIfNull(pieceGetter);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _pieceGetter = pieceGetter;
            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _playerPiecesBag = playerPiecesBag;
        }

        public void Initialize()
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?
        }

        public bool Resolve()
        {
            if (_playerPiecesBag.Current.HasValue)
            {
                return false;
            }

            _playerPiecesBag.PrepareNext();

            InvalidOperationException.ThrowIfNull(_playerPiecesBag.Current);

            PieceType pieceType = _playerPiecesBag.Current.Value;
            IPiece piece = _pieceGetter.Get(pieceType);

            _eventEnqueuer.Enqueue(_eventFactory.GetInstantiatePlayerPieceEvent(piece, pieceType));

            return true;
        }
    }
}