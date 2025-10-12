using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Game.Gameplay.View.EventResolution.EventResolvers.Actions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.EventResolution.EventResolvers
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
        IAction GetSetCameraRowAction(int topRow);
    }
}