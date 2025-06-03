using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.PhaseResolution.Phases;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.PhaseResolution
{
    public class PhaseResolver : IPhaseResolver
    {
        [NotNull] private readonly IGoalsCompletedPhase _goalsCompletedPhase;
        [NotNull] private readonly IInstantiateInitialPiecesPhase _instantiateInitialPiecesPhase;
        [NotNull] private readonly ILockPlayerPiecePhase _lockPlayerPiecePhase;
        [NotNull] private readonly IDestroyNotAlivePiecesPhase _destroyNotAlivePiecesPhase;
        [NotNull] private readonly IGravityPhase _gravityPhase;
        [NotNull] private readonly ILineClearPhase _lineClearPhase;
        [NotNull] private readonly IInstantiatePlayerPiecePhase _instantiatePlayerPiecePhase;

        [NotNull, ItemNotNull] private readonly IReadOnlyList<IPhase> _phases;

        public PhaseResolver(
            [NotNull] IGoalsCompletedPhase goalsCompletedPhase,
            [NotNull] IInstantiateInitialPiecesPhase instantiateInitialPiecesPhase,
            [NotNull] ILockPlayerPiecePhase lockPlayerPiecePhase,
            [NotNull] IDestroyNotAlivePiecesPhase destroyNotAlivePiecesPhase,
            [NotNull] IGravityPhase gravityPhase,
            [NotNull] ILineClearPhase lineClearPhase,
            [NotNull] IInstantiatePlayerPiecePhase instantiatePlayerPiecePhase)
        {
            ArgumentNullException.ThrowIfNull(goalsCompletedPhase);
            ArgumentNullException.ThrowIfNull(instantiateInitialPiecesPhase);
            ArgumentNullException.ThrowIfNull(lockPlayerPiecePhase);
            ArgumentNullException.ThrowIfNull(destroyNotAlivePiecesPhase);
            ArgumentNullException.ThrowIfNull(gravityPhase);
            ArgumentNullException.ThrowIfNull(lineClearPhase);
            ArgumentNullException.ThrowIfNull(instantiatePlayerPiecePhase);

            _goalsCompletedPhase = goalsCompletedPhase;
            _instantiateInitialPiecesPhase = instantiateInitialPiecesPhase;
            _lockPlayerPiecePhase = lockPlayerPiecePhase;
            _destroyNotAlivePiecesPhase = destroyNotAlivePiecesPhase;
            _gravityPhase = gravityPhase;
            _lineClearPhase = lineClearPhase;
            _instantiatePlayerPiecePhase = instantiatePlayerPiecePhase;

            _phases = new List<IPhase>
            {
                _goalsCompletedPhase,
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

            _goalsCompletedPhase.Initialize();
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

                ResolvePhase(phase, resolveContext, ref index);
            }

            NotifyEndIteration();
        }

        private static void ResolvePhase([NotNull] IPhase phase, ResolveContext resolveContext, ref int index)
        {
            ArgumentNullException.ThrowIfNull(phase);

            ResolveResult resolveResult = phase.Resolve(resolveContext);

            switch (resolveResult)
            {
                case ResolveResult.Updated:
                    index = 0;
                    break;
                case ResolveResult.NotUpdated:
                    ++index;
                    break;
                case ResolveResult.Stop:
                    index = int.MaxValue;
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(resolveResult);
                    return;
            }
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