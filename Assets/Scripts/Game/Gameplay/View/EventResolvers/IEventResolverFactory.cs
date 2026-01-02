using Game.Gameplay.Events.Events;
using Game.Gameplay.View.EventResolvers.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers
{
    public interface IEventResolverFactory
    {
        [NotNull]
        IEventResolver<InstantiatePieceEvent> GetInstantiatePieceEventResolver();

        [NotNull]
        IEventResolver<InstantiatePlayerPieceEvent> GetInstantiatePlayerPieceEventResolver();

        [NotNull]
        IEventResolver<InstantiatePlayerPieceGhostEvent> GetInstantiatePlayerPieceGhostEventResolver();

        [NotNull]
        IEventResolver<LockPlayerPieceEvent> GetLockPlayerPieceEventResolver();

        [NotNull]
        IEventResolver<DamagePieceEvent> GetDamagePieceEventResolver();

        [NotNull]
        IEventResolver<DamagePiecesByLineClearEvent> GetDamagePiecesByLineClearEventResolver();

        [NotNull]
        IEventResolver<DestroyPieceEvent> GetDestroyPieceEventResolver();

        [NotNull]
        IEventResolver<DestroyPlayerPieceEvent> GetDestroyPlayerPieceEventResolver();

        [NotNull]
        IEventResolver<DestroyPlayerPieceGhostEvent> GetDestroyPlayerPieceGhostEventResolver();

        [NotNull]
        IEventResolver<MovePieceEvent> GetMovePieceEventResolver();

        [NotNull]
        IEventResolver<MovePlayerPieceEvent> GetMovePlayerPieceEventResolver();

        [NotNull]
        IEventResolver<MovePiecesByGravityEvent> GetMovePiecesByGravityEventResolver();

        [NotNull]
        IEventResolver<MoveCameraEvent> GetMoveCameraEventResolver();

        [NotNull]
        IEventResolver<InstantiateInitialPiecesAndMoveCameraEvent> GetInstantiateInitialPiecesAndMoveCameraEventResolver();

        [NotNull]
        IEventResolver<SetGoalCurrentAmountEvent> GetSetGoalCurrentAmountEventResolver();

        [NotNull]
        IEventResolver<SetMovesAmountEvent> GetSetMovesAmountEventResolver();

        [NotNull]
        IEventResolver<SwapCurrentNextPlayerPieceEvent> GetSwapCurrentNextPlayerPieceEventResolver();
    }
}