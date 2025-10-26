using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Actions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolvers.EventResolvers
{
    public interface IActionFactory
    {
        [NotNull]
        IAction GetInstantiatePieceAction(
            IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            Coordinate sourceCoordinate
        );

        [NotNull]
        IAction GetInstantiatePlayerPieceAction(IPiece piece, InstantiatePieceReason instantiatePieceReason);

        [NotNull]
        IAction GetDestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason);

        [NotNull]
        IAction GetDamagePieceAction(
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason
        );

        [NotNull]
        IAction GetDestroyPieceAction(int pieceId, DestroyPieceReason destroyPieceReason);

        [NotNull]
        IAction GetMovePieceAction(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IAction GetMovePlayerPieceAction(int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IAction GetMoveCameraAction(int rowOffset);

        [NotNull]
        IAction GetSetGoalCurrentAmountAction(PieceType pieceType, int currentAmount, Coordinate coordinate);

        [NotNull]
        IAction GetSetMovesAmountAction(int amount);

        [NotNull]
        IAction GetParallelActionGroup(IEnumerable<IAction> actions, float secondsBetweenActions);
    }
}