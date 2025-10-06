using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Goals;
using Game.Gameplay.Goals.Parsing;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public class GameplaySerializedDataConverter : IGameplaySerializedDataConverter
    {
        [NotNull] private readonly IBoardSerializedDataConverter _boardSerializedDataConverter;
        [NotNull] private readonly IGoalsSerializedDataConverter _goalsSerializedDataConverter;

        public GameplaySerializedDataConverter(
            [NotNull] IBoardSerializedDataConverter boardSerializedDataConverter,
            [NotNull] IGoalsSerializedDataConverter goalsSerializedDataConverter)
        {
            ArgumentNullException.ThrowIfNull(boardSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(goalsSerializedDataConverter);

            _boardSerializedDataConverter = boardSerializedDataConverter;
            _goalsSerializedDataConverter = goalsSerializedDataConverter;
        }

        public void To(
            [NotNull] GameplaySerializedData gameplaySerializedData,
            out IBoard board,
            out IEnumerable<PiecePlacement> piecePlacements,
            out IGoals goals)
        {
            ArgumentNullException.ThrowIfNull(gameplaySerializedData);

            _boardSerializedDataConverter.To(gameplaySerializedData.BoardSerializedData, out board, out piecePlacements);
            goals = _goalsSerializedDataConverter.To(gameplaySerializedData.GoalsSerializedData);
        }

        public GameplaySerializedData From(IBoard board, IGoals goals)
        {
            return
                new GameplaySerializedData
                {
                    BoardSerializedData = _boardSerializedDataConverter.From(board),
                    GoalsSerializedData = _goalsSerializedDataConverter.From(goals)
                };
        }
    }
}