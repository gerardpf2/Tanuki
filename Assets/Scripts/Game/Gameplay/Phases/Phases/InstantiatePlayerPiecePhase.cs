using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class InstantiatePlayerPiecePhase : BaseInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;

        protected override int? MaxResolveTimesPerIteration => 1;

        public InstantiatePlayerPiecePhase(
            [NotNull] IBag bag,
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer) : base(bag, board, camera)
        {
            ArgumentNullException.ThrowIfNull(eventEnqueuer);

            _eventEnqueuer = eventEnqueuer;
        }

        protected override ResolveResult ResolveImpl(
            [NotNull] ResolveContext resolveContext,
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            resolveContext.SetPieceSourceCoordinate(sourceCoordinate, lockSourceCoordinate);

            InstantiatePlayerPieceEvent instantiatePlayerPieceEvent = new(piece, sourceCoordinate);

            _eventEnqueuer.Enqueue(instantiatePlayerPieceEvent);

            return ResolveResult.Updated;
        }
    }
}