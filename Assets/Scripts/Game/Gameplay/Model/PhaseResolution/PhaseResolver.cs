using System.Collections.Generic;
using Game.Gameplay.Model.Board;
using Game.Gameplay.Model.PhaseResolution.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Model.PhaseResolution
{
    public class PhaseResolver : IPhaseResolver
    {
        [NotNull] private readonly IInitializePhase _initializePhase;

        public PhaseResolver([NotNull] IInitializePhase initializePhase)
        {
            ArgumentNullException.ThrowIfNull(initializePhase);

            _initializePhase = initializePhase;
        }

        public void InitializeAndResolve(IBoard board, IEnumerable<IPiecePlacement> piecePlacements)
        {
            _initializePhase.Resolve(board, piecePlacements);

            Resolve(board);
        }

        public void Resolve(IBoard board) { }
    }
}