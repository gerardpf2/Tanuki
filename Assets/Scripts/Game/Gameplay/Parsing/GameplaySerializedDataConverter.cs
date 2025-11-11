using Game.Gameplay.Bag;
using Game.Gameplay.Bag.Parsing;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Parsing;
using Game.Gameplay.Moves;
using Game.Gameplay.Moves.Parsing;
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
            IBoard board,
            IGoals goals,
            IMoves moves,
            IBag bag)
        {
            ArgumentNullException.ThrowIfNull(gameplaySerializedData);

            _boardSerializedDataConverter.To(gameplaySerializedData.BoardSerializedData, board);
            _goalsSerializedDataConverter.To(gameplaySerializedData.GoalsSerializedData, goals);
            _movesSerializedDataConverter.To(gameplaySerializedData.MovesSerializedData, moves);
            _bagSerializedDataConverter.To(gameplaySerializedData.BagSerializedData, bag);
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