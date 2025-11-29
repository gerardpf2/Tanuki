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
            return new InstantiatePlayerPieceEventResolver(_actionFactory);
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
            return new DamagePiecesByLineClearEventResolver(_board, _actionFactory);
        }

        public IEventResolver<DestroyPieceEvent> GetDestroyPieceEventResolver()
        {
            return new DestroyPieceEventResolver(_actionFactory);
        }

        public IEventResolver<MovePieceEvent> GetMovePieceEventResolver()
        {
            return new MovePieceEventResolver(_actionFactory);
        }

        public IEventResolver<MovePiecesByGravityEvent> GetMovePiecesByGravityEventResolver()
        {
            return new MovePiecesByGravityEventResolver(_board, _actionFactory, _coroutineRunner);
        }

        public IEventResolver<MoveCameraEvent> GetMoveCameraEventResolver()
        {
            return new MoveCameraEventResolver(_actionFactory);
        }
    }
}