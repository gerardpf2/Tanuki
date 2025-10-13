using System;
using Game.Gameplay.EventEnqueueing.Events;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Game.Gameplay.View.EventResolution
{
    public class EventsResolverSingle : IEventsResolverSingle
    {
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public EventsResolverSingle([NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _eventResolverFactory = eventResolverFactory;
        }

        public void Resolve([NotNull] IEvent evt, Action onComplete)
        {
            ArgumentNullException.ThrowIfNull(evt);

            switch (evt)
            {
                case InstantiatePieceEvent instantiateEvent:
                    _eventResolverFactory.GetInstantiatePieceEventResolver().Resolve(instantiateEvent, onComplete);
                    break;
                case InstantiatePlayerPieceEvent instantiatePlayerPieceEvent:
                    _eventResolverFactory.GetInstantiatePlayerPieceEventResolver().Resolve(instantiatePlayerPieceEvent, onComplete);
                    break;
                case LockPlayerPieceEvent lockPlayerPieceEvent:
                    _eventResolverFactory.GetLockPlayerPieceEventResolver().Resolve(lockPlayerPieceEvent, onComplete);
                    break;
                case DamagePieceEvent damagePieceEvent:
                    _eventResolverFactory.GetDamagePieceEventResolver().Resolve(damagePieceEvent, onComplete);
                    break;
                case DestroyPieceEvent destroyPieceEvent:
                    _eventResolverFactory.GetDestroyPieceEventResolver().Resolve(destroyPieceEvent, onComplete);
                    break;
                case MovePieceEvent movePieceEvent:
                    _eventResolverFactory.GetMovePieceEventResolver().Resolve(movePieceEvent, onComplete);
                    break;
                case SetCameraRowEvent setCameraRowEvent:
                    _eventResolverFactory.GetSetCameraRowEventResolver().Resolve(setCameraRowEvent, onComplete);
                    break;
                case SetGoalCurrentAmountEvent setGoalCurrentAmountEvent:
                    _eventResolverFactory.GetSetGoalCurrentAmountEventResolver().Resolve(setGoalCurrentAmountEvent, onComplete);
                    break;
                case SetMovesAmountEvent setMovesAmountEvent:
                    _eventResolverFactory.GetSetMovesAmountEventResolver().Resolve(setMovesAmountEvent, onComplete);
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(evt);
                    return;
            }
        }
    }
}