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

        public PhaseResolver([NotNull] IInstantiateInitial instantiateInitial)
        {
            ArgumentNullException.ThrowIfNull(instantiateInitial);

            _instantiateInitial = instantiateInitial;
        }

        public void ResolveInstantiateInitial(IBoard board, IEnumerable<IPiecePlacement> piecePlacements)
        {
            _instantiateInitial.Resolve(board, piecePlacements);

            Resolve(board);
        }

        public void Resolve(IBoard board) { }
    }
}