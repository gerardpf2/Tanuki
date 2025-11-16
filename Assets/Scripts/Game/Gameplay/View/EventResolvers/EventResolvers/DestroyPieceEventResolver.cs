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

        public DestroyPieceEventResolver([NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(actionFactory);

            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DestroyPieceEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData = evt.GoalData;

            if (goalData is not null)
            {
                yield return GetGoalDataAction(goalData);
            }

            yield return _actionFactory.GetDestroyPieceAction(evt.PieceId, evt.DestroyPieceReason);

            DestroyPieceEvent.DecomposePieceData decomposeData = evt.DecomposeData;

            if (decomposeData is not null)
            {
                yield return GetDecomposeDataAction(decomposeData);
            }
        }

        [NotNull]
        private IAction GetGoalDataAction([NotNull] DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData)
        {
            ArgumentNullException.ThrowIfNull(goalData);

            return
                _actionFactory.GetSetGoalCurrentAmountAction(
                    goalData.PieceType,
                    goalData.CurrentAmount,
                    goalData.Coordinate
                );
        }

        [NotNull]
        private IAction GetDecomposeDataAction([NotNull] DestroyPieceEvent.DecomposePieceData decomposeData)
        {
            ArgumentNullException.ThrowIfNull(decomposeData);

            IEnumerable<IAction> actions = decomposeData.PiecePlacements.Select(GetInstantiatePieceAction);

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