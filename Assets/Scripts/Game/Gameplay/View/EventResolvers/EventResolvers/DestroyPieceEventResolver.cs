using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces;
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

            UpdateGoalEvent updateGoalEvent = evt.UpdateGoalEvent;

            if (updateGoalEvent is not null)
            {
                yield return
                    _actionFactory.GetEventResolverAction(
                        _eventResolverFactory.GetUpdateGoalEventResolver(),
                        updateGoalEvent
                    );
            }

            yield return _actionFactory.GetDestroyPieceAction(evt.PieceId, evt.DestroyPieceReason);

            DecomposePieceData decomposePieceData = evt.DecomposePieceData;

            if (decomposePieceData is not null)
            {
                yield return GetDecomposePieceDataAction(decomposePieceData);
            }
        }

        [NotNull]
        private IAction GetDecomposePieceDataAction([NotNull] DecomposePieceData decomposePieceData)
        {
            ArgumentNullException.ThrowIfNull(decomposePieceData);

            IEnumerable<IAction> actions = decomposePieceData.PiecePlacements.Select(GetInstantiatePieceAction);

            return _actionFactory.GetParallelActionGroup(actions);

            [NotNull]
            IAction GetInstantiatePieceAction([NotNull] PiecePlacement piecePlacement)
            {
                ArgumentNullException.ThrowIfNull(piecePlacement);

                return
                    _actionFactory.GetInstantiatePieceAction(
                        piecePlacement.Piece,
                        InstantiatePieceReason.Decompose,
                        piecePlacement.Coordinate
                    );
            }
        }
    }
}