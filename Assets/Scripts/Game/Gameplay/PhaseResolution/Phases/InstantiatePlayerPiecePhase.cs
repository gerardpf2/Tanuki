using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing;
using Game.Gameplay.Player;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution.Phases
{
    public class InstantiatePlayerPiecePhase : Phase, IInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;
        [NotNull] private readonly IPlayerPiecesBag _playerPiecesBag;

        public InstantiatePlayerPiecePhase(
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory,
            [NotNull] IPlayerPiecesBag playerPiecesBag) : base(-1, 1)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);
            ArgumentNullException.ThrowIfNull(playerPiecesBag);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
            _playerPiecesBag = playerPiecesBag;
        }

        public void Initialize()
        {
            // TODO: Remove if not needed

            Uninitialize();
        }

        public override void Uninitialize()
        {
            // TODO: Remove if not needed

            base.Uninitialize();
        }

        protected override ResolveResult ResolveImpl(ResolveContext _)
        {
            if (_playerPiecesBag.Current is not null)
            {
                return ResolveResult.NotUpdated;
            }

            _playerPiecesBag.PrepareNext();

            IPiece piece = _playerPiecesBag.Current;

            _eventEnqueuer.Enqueue(_eventFactory.GetInstantiatePlayerPieceEvent(piece));

            return ResolveResult.Updated;
        }
    }
}