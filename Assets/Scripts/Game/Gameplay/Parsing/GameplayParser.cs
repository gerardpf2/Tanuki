using System.Collections.Generic;
using Game.Gameplay.Bag;
using Game.Gameplay.Board;
using Game.Gameplay.Goals;
using Game.Gameplay.Moves;
using Game.Gameplay.Pieces;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using JetBrains.Annotations;

namespace Game.Gameplay.Parsing
{
    public class GameplayParser : IGameplayParser
    {
        [NotNull] private readonly IGameplaySerializedDataConverter _gameplaySerializedDataConverter;
        [NotNull] private readonly IParser _parser;

        public GameplayParser(
            [NotNull] IGameplaySerializedDataConverter gameplaySerializedDataConverter,
            [NotNull] IParser parser)
        {
            ArgumentNullException.ThrowIfNull(gameplaySerializedDataConverter);
            ArgumentNullException.ThrowIfNull(parser);

            _gameplaySerializedDataConverter = gameplaySerializedDataConverter;
            _parser = parser;
        }

        public string Serialize(IBoard board, IGoals goals, IMoves moves, IBag bag)
        {
            GameplaySerializedData gameplaySerializedData =
                _gameplaySerializedDataConverter.From(
                    board,
                    goals,
                    moves,
                    bag
                );

            return _parser.Serialize(gameplaySerializedData);
        }

        public void Deserialize(
            string value,
            out IBoard board,
            out IEnumerable<PiecePlacement> piecePlacements,
            out IGoals goals,
            out IMoves moves,
            out IBag bag)
        {
            GameplaySerializedData gameplaySerializedData = _parser.Deserialize<GameplaySerializedData>(value);

            _gameplaySerializedDataConverter.To(
                gameplaySerializedData,
                out board,
                out piecePlacements,
                out goals,
                out moves,
                out bag
            );
        }
    }
}