using System.Collections.Generic;
using Game.Gameplay.Bag;
using Game.Gameplay.Bag.Parsing;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.Moves;
using Game.Gameplay.Moves.Parsing;
using Game.Gameplay.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public class GameplaySerializedDataConverter : IGameplaySerializedDataConverter
    {
        [NotNull] private readonly IBoardSerializedDataConverter _boardSerializedDataConverter;
        [NotNull] private readonly IGoalsSerializedDataConverter _goalsSerializedDataConverter;
        [NotNull] private readonly IMovesSerializedDataConverter _movesSerializedDataConverter;
        [NotNull] private readonly IBagSerializedDataConverter _bagSerializedDataConverter;

        public GameplaySerializedDataConverter(
            [NotNull] IBoardSerializedDataConverter boardSerializedDataConverter,
            [NotNull] IGoalsSerializedDataConverter goalsSerializedDataConverter,
            [NotNull] IMovesSerializedDataConverter movesSerializedDataConverter,
            [NotNull] IBagSerializedDataConverter bagSerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(boardSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(goalsSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(movesSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(bagSerializedDataConverter);

            _boardSerializedDataConverter = boardSerializedDataConverter;
            _goalsSerializedDataConverter = goalsSerializedDataConverter;
            _movesSerializedDataConverter = movesSerializedDataConverter;
            _bagSerializedDataConverter = bagSerializedDataConverter;
        }

        public void To(
            [NotNull] GameplaySerializedData gameplaySerializedData,
            out IBoard board,
            out IEnumerable<PiecePlacement> piecePlacements,
            out IGoals goals,
            out IMoves moves,
            out IBag bag)
        {
            ArgumentNullException.ThrowIfNull(gameplaySerializedData);

            _boardSerializedDataConverter.To(gameplaySerializedData.BoardSerializedData, out board, out piecePlacements);
            goals = _goalsSerializedDataConverter.To(gameplaySerializedData.GoalsSerializedData);
            moves = _movesSerializedDataConverter.To(gameplaySerializedData.MovesSerializedData);
            bag = _bagSerializedDataConverter.To(gameplaySerializedData.BagSerializedData);
        }

        public GameplaySerializedData From(IBoard board, IGoals goals, IMoves moves, IBag bag)
        {
            return
                new GameplaySerializedData
                {
                    BoardSerializedData = _boardSerializedDataConverter.From(board),
                    GoalsSerializedData = _goalsSerializedDataConverter.From(goals),
                    MovesSerializedData = _movesSerializedDataConverter.From(moves),
                    BagSerializedData = _bagSerializedDataConverter.From(bag)
                };
        }
    }
}