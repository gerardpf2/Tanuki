using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.PhaseResolution.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution
{
    public class PhaseResolver : IPhaseResolver
    {
        [NotNull] private readonly IInstantiateInitialPiecesPhase _instantiateInitialPiecesPhase;
        [NotNull] private readonly IInstantiatePlayerPiecePhase _instantiatePlayerPiecePhase;

        public PhaseResolver(
            [NotNull] IInstantiateInitialPiecesPhase instantiateInitialPiecesPhase,
            [NotNull] IInstantiatePlayerPiecePhase instantiatePlayerPiecePhase)
        {
            ArgumentNullException.ThrowIfNull(instantiateInitialPiecesPhase);
            ArgumentNullException.ThrowIfNull(instantiatePlayerPiecePhase);

            _instantiateInitialPiecesPhase = instantiateInitialPiecesPhase;
            _instantiatePlayerPiecePhase = instantiatePlayerPiecePhase;
        }

        public void ResolveInstantiateInitialAndCascade(IBoard board, IEnumerable<IPiecePlacement> piecePlacements)
        {
            _instantiateInitialPiecesPhase.Resolve(board, piecePlacements);

            ResolveCascade(board);
        }

        public void ResolveCascade(IBoard board)
        {
            _instantiatePlayerPiecePhase.Resolve();
        }
    }
}