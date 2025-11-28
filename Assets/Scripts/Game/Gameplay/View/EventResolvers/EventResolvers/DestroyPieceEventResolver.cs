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

            UpdateGoalData updateGoalData = evt.UpdateGoalData;

            if (updateGoalData is not null)
            {
                yield return GetUpdateGoalDataAction(updateGoalData);
            }

            yield return _actionFactory.GetDestroyPieceAction(evt.PieceId, evt.DestroyPieceReason);

            DecomposePieceData decomposePieceData = evt.DecomposePieceData;

            if (decomposePieceData is not null)
            {
                yield return GetDecomposePieceDataAction(decomposePieceData);
            }
        }

        [NotNull]
        private IAction GetUpdateGoalDataAction([NotNull] UpdateGoalData updateGoalData)
        {
            ArgumentNullException.ThrowIfNull(updateGoalData);

            return
                _actionFactory.GetSetGoalCurrentAmountAction(
                    updateGoalData.PieceType,
                    updateGoalData.CurrentAmount,
                    updateGoalData.Coordinate
                );
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