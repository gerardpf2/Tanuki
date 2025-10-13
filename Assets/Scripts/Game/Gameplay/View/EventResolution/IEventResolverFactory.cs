using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.View.EventResolution.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution
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
        IEventResolver<DestroyPieceEvent> GetDestroyPieceEventResolver();

        [NotNull]
        IEventResolver<MovePieceEvent> GetMovePieceEventResolver();

        [NotNull]
        IEventResolver<SetCameraRowEvent> GetSetCameraRowEventResolver();

        [NotNull]
        IEventResolver<SetGoalCurrentAmountEvent> GetSetGoalCurrentAmountEventResolver();
    }
}