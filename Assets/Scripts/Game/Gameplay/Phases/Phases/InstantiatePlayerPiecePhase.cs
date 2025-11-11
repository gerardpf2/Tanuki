using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class InstantiatePlayerPiecePhase : BaseInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;
        [NotNull] private readonly IEventFactory _eventFactory;

        protected override int? MaxResolveTimesPerIteration => 1;

        public InstantiatePlayerPiecePhase(
            [NotNull] IBagContainer bagContainer,
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer,
            [NotNull] IEventFactory eventFactory) : base(bagContainer, board, camera)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);
            ArgumentNullException.ThrowIfNull(eventFactory);

            _eventEnqueuer = eventEnqueuer;
            _eventFactory = eventFactory;
        }

        protected override ResolveResult ResolveImpl(
            [NotNull] ResolveContext resolveContext,
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            resolveContext.SetPieceSourceCoordinate(sourceCoordinate, lockSourceCoordinate);

            _eventEnqueuer.Enqueue(_eventFactory.GetInstantiatePlayerPieceEvent(piece, sourceCoordinate));

            return ResolveResult.Updated;
        }
    }
}