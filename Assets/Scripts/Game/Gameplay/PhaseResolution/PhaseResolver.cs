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
        [NotNull] private readonly ILockPlayerPiecePhase _lockPlayerPiecePhase;
        [NotNull] private readonly IInstantiatePlayerPiecePhase _instantiatePlayerPiecePhase;
        [NotNull, ItemNotNull] private readonly IReadOnlyList<IPhase> _phases;

        public PhaseResolver(
            [NotNull] IInstantiateInitialPiecesPhase instantiateInitialPiecesPhase,
            [NotNull] ILockPlayerPiecePhase lockPlayerPiecePhase,
            [NotNull] IInstantiatePlayerPiecePhase instantiatePlayerPiecePhase)
        {
            ArgumentNullException.ThrowIfNull(instantiateInitialPiecesPhase);
            ArgumentNullException.ThrowIfNull(lockPlayerPiecePhase);
            ArgumentNullException.ThrowIfNull(instantiatePlayerPiecePhase);

            _instantiateInitialPiecesPhase = instantiateInitialPiecesPhase;
            _lockPlayerPiecePhase = lockPlayerPiecePhase;
            _instantiatePlayerPiecePhase = instantiatePlayerPiecePhase;

            _phases = new List<IPhase>
            {
                _instantiateInitialPiecesPhase,
                _lockPlayerPiecePhase,
                _instantiatePlayerPiecePhase
            };
        }

        public void Initialize(IBoard board, IEnumerable<IPiecePlacement> piecePlacements)
        {
            // TODO: Check allow multiple Initialize. Add Clear ¿?

            _instantiateInitialPiecesPhase.Initialize(board, piecePlacements);
            _lockPlayerPiecePhase.Initialize();
            _instantiatePlayerPiecePhase.Initialize();
        }

        public void Resolve()
        {
            int index = 0;

            while (index < _phases.Count)
            {
                IPhase phase = _phases[index];

                bool resolved = phase.Resolve();

                index = resolved ? 0 : index + 1;
            }
        }
    }
}