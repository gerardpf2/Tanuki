using System.Collections.Generic;
using Game.Common;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public class DamagePieceHelper : IDamagePieceHelper
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IGoals _goals;
        [NotNull] private readonly IPieceGetter _pieceGetter;

        public DamagePieceHelper([NotNull] IBoard board, [NotNull] IGoals goals, [NotNull] IPieceGetter pieceGetter)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(goals);
            ArgumentNullException.ThrowIfNull(pieceGetter);

            _board = board;
            _goals = goals;
            _pieceGetter = pieceGetter;
        }

        public DamagePieceEvent Damage(
            int pieceId,
            Coordinate coordinate,
            DamagePieceReason damagePieceReason,
            Direction direction)
        {
            IPiece piece = _board.GetPiece(pieceId);

            _board.GetPieceRowColumnOffset(
                pieceId,
                coordinate.Row,
                coordinate.Column,
                out int rowOffset,
                out int columnOffset
            );

            piece.Damage(rowOffset, columnOffset);

            DestroyPieceEvent destroyPieceEvent = DestroyIfNeeded(piece);

            DamagePieceEvent damagePieceEvent =
                new(
                    destroyPieceEvent,
                    pieceId,
                    piece.State,
                    damagePieceReason,
                    direction
                );

            return damagePieceEvent;
        }

        private DestroyPieceEvent DestroyIfNeeded([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (piece.Alive)
            {
                return null;
            }

            int pieceId = piece.Id;
            Coordinate sourceCoordinate = _board.GetSourceCoordinate(pieceId);

            SetGoalCurrentAmountEvent setGoalCurrentAmountEvent =
                IncreaseGoalCurrentAmountIfNeeded(
                    piece.Type,
                    sourceCoordinate
                );

            _board.RemovePiece(pieceId);

            IEnumerable<InstantiatePieceEvent> instantiatePieceEventsDecompose =
                DecomposeIfNeeded(
                    piece,
                    sourceCoordinate
                );

            DestroyPieceEvent destroyPieceEvent =
                new(
                    setGoalCurrentAmountEvent,
                    instantiatePieceEventsDecompose,
                    pieceId,
                    DestroyPieceReason.NotAlive
                );

            return destroyPieceEvent;
        }

        private SetGoalCurrentAmountEvent IncreaseGoalCurrentAmountIfNeeded(
            PieceType pieceType,
            Coordinate sourceCoordinate)
        {
            if (!_goals.TryIncreaseCurrentAmount(pieceType, out int goalCurrentAmount))
            {
                return null;
            }

            SetGoalCurrentAmountEvent setGoalCurrentAmountEvent = new(pieceType, goalCurrentAmount, sourceCoordinate); // TODO: Use center coordinate instead ¿?

            return setGoalCurrentAmountEvent;
        }

        [ItemNotNull]
        private IEnumerable<InstantiatePieceEvent> DecomposeIfNeeded(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!piece.DecomposeType.HasValue)
            {
                yield break;
            }

            PieceType decomposeType = piece.DecomposeType.Value;

            foreach (Coordinate decomposeSourceCoordinate in piece.GetUndamagedCoordinates(sourceCoordinate))
            {
                IPiece decomposePiece = _pieceGetter.Get(decomposeType);

                _board.AddPiece(decomposePiece, decomposeSourceCoordinate);

                InstantiatePieceEvent instantiatePieceEvent =
                    new(
                        decomposePiece,
                        decomposeSourceCoordinate,
                        InstantiatePieceReason.Decompose
                    );

                yield return instantiatePieceEvent;
            }
        }
    }
}