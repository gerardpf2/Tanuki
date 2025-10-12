using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Parsing
{
    public class MovesParser : IMovesParser
    {
        [NotNull] private readonly IMovesSerializedDataConverter _movesSerializedDataConverter;
        [NotNull] private readonly IParser _parser;

        public MovesParser(
            [NotNull] IMovesSerializedDataConverter movesSerializedDataConverter,
            [NotNull] IParser parser)
        {
            ArgumentNullException.ThrowIfNull(movesSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(parser);

            _movesSerializedDataConverter = movesSerializedDataConverter;
            _parser = parser;
        }

        public string Serialize(IMoves moves)
        {
            MovesSerializedData movesSerializedData = _movesSerializedDataConverter.From(moves);

            return _parser.Serialize(movesSerializedData);
        }

        public IMoves Deserialize(string value)
        {
            MovesSerializedData movesSerializedData = _parser.Deserialize<MovesSerializedData>(value);

            return _movesSerializedDataConverter.To(movesSerializedData);
        }
    }
}