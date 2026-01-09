using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Phases.Phases
{
    public class SimulateInstantiatePlayerPiecePhase : BaseInstantiatePlayerPiecePhase
    {
        public SimulateInstantiatePlayerPiecePhase([NotNull] IBag bag, [NotNull] IBoard board, [NotNull] ICamera camera) : base(bag, board, camera) { }

        protected override ResolveResult ResolveImpl(
            [NotNull] ResolveContext resolveContext,
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(resolveContext);

            if (resolveContext.PieceSourceCoordinate.HasValue && resolveContext.PieceLockSourceCoordinate.HasValue)
            {
                Coordinate prevSourceCoordinate = resolveContext.PieceSourceCoordinate.Value;
                Coordinate prevLockSourceCoordinate = resolveContext.PieceLockSourceCoordinate.Value;

                if (sourceCoordinate.Equals(prevSourceCoordinate) &&
                    lockSourceCoordinate.Equals(prevLockSourceCoordinate))
                {
                    return ResolveResult.NotUpdated;
                }
            }

            resolveContext.SetPieceSourceCoordinate(sourceCoordinate, lockSourceCoordinate);

            return ResolveResult.Updated;
        }
    }
}