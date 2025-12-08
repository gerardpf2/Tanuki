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
        IEventResolver<LockPlayerPieceEvent> GetLockPlayerPieceEventResolver();

        [NotNull]
        IEventResolver<DamagePieceEvent> GetDamagePieceEventResolver();

        [NotNull]
        IEventResolver<DamagePiecesByLineClearEvent> GetDamagePiecesByLineClearEventResolver();

        [NotNull]
        IEventResolver<DestroyPieceEvent> GetDestroyPieceEventResolver();

        [NotNull]
        IEventResolver<MovePieceEvent> GetMovePieceEventResolver();

        [NotNull]
        IEventResolver<MovePiecesByGravityEvent> GetMovePiecesByGravityEventResolver();

        [NotNull]
        IEventResolver<MoveCameraEvent> GetMoveCameraEventResolver();

        [NotNull]
        IEventResolver<SetGoalCurrentAmountEvent> GetSetGoalCurrentAmountEventResolver();
    }
}