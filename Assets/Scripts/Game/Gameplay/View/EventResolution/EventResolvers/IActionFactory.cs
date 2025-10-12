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
        IAction GetDamagePieceAction(int id, IEnumerable<KeyValuePair<string, string>> state);

        [NotNull]
        IAction GetDestroyPieceAction(int id, DestroyPieceReason destroyPieceReason);

        [NotNull]
        IAction GetMovePieceAction(int id, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IAction GetSetCameraRowAction(int topRow);
    }
}