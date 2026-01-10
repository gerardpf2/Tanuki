using Game.Common.Pieces;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class InstantiatePlayerPiecePhase : BaseInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IBag _bag;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;

        protected override int? MaxResolveTimesPerIteration => 1;

        public InstantiatePlayerPiecePhase(
            [NotNull] IBag bag,
            [NotNull] IBoard board,
            [NotNull] ICamera camera,
            [NotNull] IEventEnqueuer eventEnqueuer) : base(bag, board, camera)
        {
            ArgumentNullException.ThrowIfNull(bag);
            ArgumentNullException.ThrowIfNull(eventEnqueuer);

            _bag = bag;
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

            PieceType nextPieceType = _bag.Next.Type;

            InstantiatePlayerPieceEvent instantiatePlayerPieceEvent =
                new(
                    piece,
                    sourceCoordinate,
                    nextPieceType,
                    InstantiatePieceReason.Regular
                );

            _eventEnqueuer.Enqueue(instantiatePlayerPieceEvent);

            return ResolveResult.Updated;
        }
    }
}