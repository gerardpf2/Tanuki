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
        [NotNull] private readonly IGameplaySerializedDataConverter _gameplaySerializedDataConverter;
        [NotNull] private readonly IParser _parser;

        public GameplayParser(
            IBoard board,
            [NotNull] IGameplaySerializedDataConverter gameplaySerializedDataConverter,
            [NotNull] IParser parser)
        {
            ArgumentNullException.ThrowIfNull(gameplaySerializedDataConverter);
            ArgumentNullException.ThrowIfNull(parser);

            _board = board;
            _gameplaySerializedDataConverter = gameplaySerializedDataConverter;
            _parser = parser;
        }

        public string Serialize(IGoals goals, IMoves moves, IBag bag)
        {
            GameplaySerializedData gameplaySerializedData =
                _gameplaySerializedDataConverter.From(
                    _board,
                    goals,
                    moves,
                    bag
                );

            return _parser.Serialize(gameplaySerializedData);
        }

        public void Deserialize(string value, out IGoals goals, out IMoves moves, out IBag bag)
        {
            GameplaySerializedData gameplaySerializedData = _parser.Deserialize<GameplaySerializedData>(value);

            _gameplaySerializedDataConverter.To(gameplaySerializedData, _board, out goals, out moves, out bag);
        }
    }
}