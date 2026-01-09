using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Events;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class SwapCurrentNextPlayerPiecePhase : BaseInstantiatePlayerPiecePhase
    {
        [NotNull] private readonly IBag _bag;
        [NotNull] private readonly IEventEnqueuer _eventEnqueuer;

        protected override int? MaxResolveTimesPerIteration => 1;

        public SwapCurrentNextPlayerPiecePhase(
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

        protected override ResolveResult ResolveImpl(ResolveContext resolveContext)
        {
            ResetCurrentState();

            _bag.SwapCurrentNext();

            return base.ResolveImpl(resolveContext);
        }

        protected override ResolveResult ResolveImpl(
            [NotNull] ResolveContext resolveContext,
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            resolveContext.SetPieceSourceCoordinate(sourceCoordinate, lockSourceCoordinate);

            SwapCurrentNextPlayerPieceEvent swapCurrentNextPlayerPieceEvent = new(piece, sourceCoordinate);

            _eventEnqueuer.Enqueue(swapCurrentNextPlayerPieceEvent);

            return ResolveResult.Updated;
        }

        private void ResetCurrentState()
        {
            IPiece piece = _bag.Current;

            piece.ResetRotation();
        }
    }
}