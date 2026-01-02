using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.EventResolvers.EventResolvers;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers
{
    public class EventResolverFactory : IEventResolverFactory
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public EventResolverFactory(
            [NotNull] IBoard board,
            [NotNull] IActionFactory actionFactory,
            [NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _board = board;
            _actionFactory = actionFactory;
            _coroutineRunner = coroutineRunner;
        }

        public IEventResolver<InstantiatePieceEvent> GetInstantiatePieceEventResolver()
        {
            return new InstantiatePieceEventResolver(_actionFactory);
        }

        public IEventResolver<InstantiatePlayerPieceEvent> GetInstantiatePlayerPieceEventResolver()
        {
            return new InstantiatePlayerPieceEventResolver(_actionFactory, this);
        }

        public IEventResolver<InstantiatePlayerPieceGhostEvent> GetInstantiatePlayerPieceGhostEventResolver()
        {
            return new InstantiatePlayerPieceGhostEventResolver(_actionFactory);
        }

        public IEventResolver<LockPlayerPieceEvent> GetLockPlayerPieceEventResolver()
        {
            return new LockPlayerPieceEventResolver(_actionFactory, this);
        }

        public IEventResolver<DamagePieceEvent> GetDamagePieceEventResolver()
        {
            return new DamagePieceEventResolver(_actionFactory, this);
        }

        public IEventResolver<DamagePiecesByLineClearEvent> GetDamagePiecesByLineClearEventResolver()
        {
            return new DamagePiecesByLineClearEventResolver(_board, _actionFactory, this);
        }

        public IEventResolver<DestroyPieceEvent> GetDestroyPieceEventResolver()
        {
            return new DestroyPieceEventResolver(_actionFactory, this);
        }

        public IEventResolver<DestroyPlayerPieceEvent> GetDestroyPlayerPieceEventResolver()
        {
            return new DestroyPlayerPieceEventResolver(_actionFactory, this);
        }

        public IEventResolver<DestroyPlayerPieceGhostEvent> GetDestroyPlayerPieceGhostEventResolver()
        {
            return new DestroyPlayerPieceGhostEventResolver(_actionFactory);
        }

        public IEventResolver<MovePieceEvent> GetMovePieceEventResolver()
        {
            return new MovePieceEventResolver(_actionFactory);
        }

        public IEventResolver<MovePlayerPieceEvent> GetMovePlayerPieceEventResolver()
        {
            return new MovePlayerPieceEventResolver(_actionFactory);
        }

        public IEventResolver<MovePiecesByGravityEvent> GetMovePiecesByGravityEventResolver()
        {
            return new MovePiecesByGravityEventResolver(_board, _actionFactory, this, _coroutineRunner);
        }

        public IEventResolver<MoveCameraEvent> GetMoveCameraEventResolver()
        {
            return new MoveCameraEventResolver(_actionFactory);
        }

        public IEventResolver<InstantiateInitialPiecesAndMoveCameraEvent> GetInstantiateInitialPiecesAndMoveCameraEventResolver()
        {
            return new InstantiateInitialPiecesAndMoveCameraEventResolver(_actionFactory, this, _coroutineRunner);
        }

        public IEventResolver<SetGoalCurrentAmountEvent> GetSetGoalCurrentAmountEventResolver()
        {
            return new SetGoalCurrentAmountEventResolver(_actionFactory);
        }

        public IEventResolver<SetMovesAmountEvent> GetSetMovesAmountEventResolver()
        {
            return new SetMovesAmountEventResolver(_actionFactory);
        }

        public IEventResolver<SwapCurrentNextPlayerPieceEvent> GetSwapCurrentNextPlayerPieceEventResolver()
        {
            return new SwapCurrentNextPlayerPieceEventResolver(_actionFactory, this);
        }
    }
}