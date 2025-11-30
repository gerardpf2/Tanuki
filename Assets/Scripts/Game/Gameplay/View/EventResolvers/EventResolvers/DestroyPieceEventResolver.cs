using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Events.Events;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class DestroyPieceEventResolver : EventResolver<DestroyPieceEvent>
    {
        [NotNull] private readonly IActionFactory _actionFactory;
        [NotNull] private readonly IEventResolverFactory _eventResolverFactory;

        public DestroyPieceEventResolver(
            [NotNull] IActionFactory actionFactory,
            [NotNull] IEventResolverFactory eventResolverFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);
            ArgumentNullException.ThrowIfNull(eventResolverFactory);

            _actionFactory = actionFactory;
            _eventResolverFactory = eventResolverFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DestroyPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            yield return _actionFactory.GetDestroyPieceAction(evt.PieceId, evt.DestroyPieceReason);

            ICollection<IAction> actions = new List<IAction>();

            UpdateGoalEvent updateGoalEvent = evt.UpdateGoalEvent;

            if (updateGoalEvent is not null)
            {
                actions.Add(GetUpdateGoalEventAction(updateGoalEvent));
            }

            IReadOnlyCollection<InstantiatePieceEvent> instantiatePieceEventsDecompose = evt.InstantiatePieceEventsDecompose;

            if (instantiatePieceEventsDecompose?.Count > 0)
            {
                actions.Add(GetInstantiatePieceEventsDecomposeAction(instantiatePieceEventsDecompose));
            }

            if (actions.Count > 0)
            {
                yield return _actionFactory.GetParallelActionGroup(actions);
            }
        }

        [NotNull]
        private IAction GetUpdateGoalEventAction(UpdateGoalEvent updateGoalEvent)
        {
            return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetUpdateGoalEventResolver(),
                    updateGoalEvent
                );
        }

        [NotNull]
        private IAction GetInstantiatePieceEventsDecomposeAction(
            [NotNull] IEnumerable<InstantiatePieceEvent> instantiatePieceEventsDecompose)
        {
            ArgumentNullException.ThrowIfNull(instantiatePieceEventsDecompose);

            IEnumerable<IAction> instantiatePieceEventActions = instantiatePieceEventsDecompose.Select(GetInstantiatePieceEventAction);

            return _actionFactory.GetParallelActionGroup(instantiatePieceEventActions);
        }

        [NotNull]
        private IAction GetInstantiatePieceEventAction(InstantiatePieceEvent instantiatePieceEvent)
        {
            return
                _actionFactory.GetEventResolverAction(
                    _eventResolverFactory.GetInstantiatePieceEventResolver(),
                    instantiatePieceEvent
                );
        }
    }
}