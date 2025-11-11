using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public class GameplayParser : IGameplayParser
    {
        private readonly IBoard _board;
        private readonly IMoves _moves;
        [NotNull] private readonly IGameplaySerializedDataConverter _gameplaySerializedDataConverter;
        [NotNull] private readonly IParser _parser;

        public GameplayParser(
            IBoard board,
            IMoves moves,
            [NotNull] IGameplaySerializedDataConverter gameplaySerializedDataConverter,
            [NotNull] IParser parser)
        {
            ArgumentNullException.ThrowIfNull(gameplaySerializedDataConverter);
            ArgumentNullException.ThrowIfNull(parser);

            _board = board;
            _moves = moves;
            _gameplaySerializedDataConverter = gameplaySerializedDataConverter;
            _parser = parser;
        }

        public string Serialize(IGoals goals, IBag bag)
        {
            GameplaySerializedData gameplaySerializedData =
                _gameplaySerializedDataConverter.From(
                    _board,
                    goals,
                    _moves,
                    bag
                );

            return _parser.Serialize(gameplaySerializedData);
        }

        public void Deserialize(string value, out IGoals goals, out IBag bag)
        {
            GameplaySerializedData gameplaySerializedData = _parser.Deserialize<GameplaySerializedData>(value);

            _gameplaySerializedDataConverter.To(gameplaySerializedData, _board, out goals, _moves, out bag);
        }
    }
}