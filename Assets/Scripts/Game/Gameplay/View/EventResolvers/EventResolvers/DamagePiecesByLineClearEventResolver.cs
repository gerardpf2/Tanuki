using System.Collections.Generic;
using System.Linq;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.View.Actions;
using Game.Gameplay.View.Actions.Actions;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public class DamagePiecesByLineClearEventResolver : EventResolver<DamagePiecesByLineClearEvent>
    {
        private const float SecondsBetweenActions = 0.05f;

        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IActionFactory _actionFactory;

        public DamagePiecesByLineClearEventResolver([NotNull] IBoard board, [NotNull] IActionFactory actionFactory)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(actionFactory);

            _board = board;
            _actionFactory = actionFactory;
        }

        protected override IEnumerable<IAction> GetActions([NotNull] DamagePiecesByLineClearEvent evt)
        {
            ArgumentNullException.ThrowIfNull(evt);

            IEnumerable<int> pieceIds = GetPieceIdsSortedByColumnThenByRow(evt.PieceIds);
            IEnumerable<IAction> damagePieceActions = pieceIds.Select(GetDamagePieceAction);

            yield return _actionFactory.GetParallelActionGroup(damagePieceActions, SecondsBetweenActions);
            yield break;

            [NotNull]
            IAction GetDamagePieceAction(int pieceId)
            {
                return
                    _actionFactory.GetDamagePieceAction(
                        pieceId,
                        evt.GetState(pieceId),
                        DamagePieceReason.LineClear,
                        Direction.Right
                    );
            }
        }

        [NotNull]
        private IEnumerable<int> GetPieceIdsSortedByColumnThenByRow([NotNull] IEnumerable<int> pieceIds)
        {
            ArgumentNullException.ThrowIfNull(pieceIds);

            return pieceIds.OrderBy(GetPieceColumn).ThenBy(GetPieceRow);

            int GetPieceRow(int pieceId)
            {
                return _board.GetSourceCoordinate(pieceId).Row;
            }

            int GetPieceColumn(int pieceId)
            {
                return _board.GetSourceCoordinate(pieceId).Column;
            }
        }
    }
}