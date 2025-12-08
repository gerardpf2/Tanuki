using System.Collections.Generic;
using Game.Common;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Actions.Actions;
using Game.Gameplay.View.EventResolvers.EventResolvers;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Actions
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
        IAction GetInstantiatePlayerPieceAction(
            IPiece piece,
            InstantiatePieceReason instantiatePieceReason,
            Coordinate sourceCoordinate
        );

        [NotNull]
        IAction GetInstantiatePlayerPieceGhostAction(IPiece piece, InstantiatePieceReason instantiatePieceReason);

        [NotNull]
        IAction GetDestroyPieceAction(int pieceId, DestroyPieceReason destroyPieceReason);

        [NotNull]
        IAction GetDestroyPlayerPieceAction(DestroyPieceReason destroyPieceReason);

        [NotNull]
        IAction GetDestroyPlayerPieceGhostAction(DestroyPieceReason destroyPieceReason);

        [NotNull]
        IAction GetDamagePieceAction(
            int pieceId,
            IEnumerable<KeyValuePair<string, string>> state,
            DamagePieceReason damagePieceReason,
            Direction direction
        );

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
        IAction GetParallelActionGroup(IEnumerable<IAction> actions, float secondsBetweenActions = 0.0f);

        [NotNull]
        IAction GetEventResolverAction<TEvent>(IEventResolver<TEvent> eventResolver, TEvent evt) where TEvent : IEvent;
    }
}