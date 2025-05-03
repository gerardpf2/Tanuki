using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.PhaseResolution.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution
{
    public class PhaseResolver : IPhaseResolver
    {
        [NotNull] private readonly IInstantiateInitial _instantiateInitial;
        [NotNull] private readonly IInstantiatePlayerPiece _instantiatePlayerPiece;

        public PhaseResolver(
            [NotNull] IInstantiateInitial instantiateInitial,
            [NotNull] IInstantiatePlayerPiece instantiatePlayerPiece)
        {
            ArgumentNullException.ThrowIfNull(instantiateInitial);
            ArgumentNullException.ThrowIfNull(instantiatePlayerPiece);

            _instantiateInitial = instantiateInitial;
            _instantiatePlayerPiece = instantiatePlayerPiece;
        }

        public void ResolveInstantiateInitialAndCascade(IBoard board, IEnumerable<IPiecePlacement> piecePlacements)
        {
            _instantiateInitial.Resolve(board, piecePlacements);

            ResolveCascade(board);
        }

        public void ResolveCascade(IBoard board)
        {
            _instantiatePlayerPiece.Resolve();
        }
    }
}