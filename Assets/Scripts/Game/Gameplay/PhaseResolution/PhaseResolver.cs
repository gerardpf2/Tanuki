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
        [NotNull] private readonly IDestroyNotAlivePiecesPhase _destroyNotAlivePiecesPhase;
        [NotNull] private readonly IGravityPhase _gravityPhase;
        [NotNull] private readonly ILineClearPhase _lineClearPhase;
        [NotNull] private readonly IInstantiatePlayerPiecePhase _instantiatePlayerPiecePhase;

        [NotNull, ItemNotNull] private readonly IReadOnlyList<IPhase> _phases;

        public PhaseResolver(
            [NotNull] IInstantiateInitialPiecesPhase instantiateInitialPiecesPhase,
            [NotNull] ILockPlayerPiecePhase lockPlayerPiecePhase,
            [NotNull] IDestroyNotAlivePiecesPhase destroyNotAlivePiecesPhase,
            [NotNull] IGravityPhase gravityPhase,
            [NotNull] ILineClearPhase lineClearPhase,
            [NotNull] IInstantiatePlayerPiecePhase instantiatePlayerPiecePhase)
        {
            ArgumentNullException.ThrowIfNull(instantiateInitialPiecesPhase);
            ArgumentNullException.ThrowIfNull(lockPlayerPiecePhase);
            ArgumentNullException.ThrowIfNull(destroyNotAlivePiecesPhase);
            ArgumentNullException.ThrowIfNull(gravityPhase);
            ArgumentNullException.ThrowIfNull(lineClearPhase);
            ArgumentNullException.ThrowIfNull(instantiatePlayerPiecePhase);

            _instantiateInitialPiecesPhase = instantiateInitialPiecesPhase;
            _lockPlayerPiecePhase = lockPlayerPiecePhase;
            _destroyNotAlivePiecesPhase = destroyNotAlivePiecesPhase;
            _gravityPhase = gravityPhase;
            _lineClearPhase = lineClearPhase;
            _instantiatePlayerPiecePhase = instantiatePlayerPiecePhase;

            _phases = new List<IPhase>
            {
                _instantiateInitialPiecesPhase,
                _lockPlayerPiecePhase,
                _destroyNotAlivePiecesPhase,
                _gravityPhase,
                _lineClearPhase,
                _instantiatePlayerPiecePhase
            };
        }

        public void Initialize(IBoard board, IEnumerable<PiecePlacement> piecePlacements)
        {
            Uninitialize();

            _instantiateInitialPiecesPhase.Initialize(board, piecePlacements);
            _lockPlayerPiecePhase.Initialize(board);
            _destroyNotAlivePiecesPhase.Initialize(board);
            _gravityPhase.Initialize(board);
            _lineClearPhase.Initialize(board);
            _instantiatePlayerPiecePhase.Initialize();
        }

        public void Uninitialize()
        {
            foreach (IPhase phase in _phases)
            {
                phase.Uninitialize();
            }
        }

        public void Resolve(ResolveContext resolveContext)
        {
            NotifyBeginIteration();

            int index = 0;

            while (index < _phases.Count)
            {
                IPhase phase = _phases[index];

                bool resolved = phase.Resolve(resolveContext);

                index = resolved ? 0 : index + 1;
            }

            NotifyEndIteration();
        }

        private void NotifyBeginIteration()
        {
            foreach (IPhase phase in _phases)
            {
                phase.OnBeginIteration();
            }
        }

        private void NotifyEndIteration()
        {
            foreach (IPhase phase in _phases)
            {
                phase.OnEndIteration();
            }
        }
    }
}