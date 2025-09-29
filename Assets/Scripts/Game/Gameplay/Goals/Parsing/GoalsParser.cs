using Infrastructure.System.Exceptions;
using Infrastructure.System.Parsing;
using JetBrains.Annotations;

namespace Game.Gameplay.Goals.Parsing
{
    public class GoalsParser : IGoalsParser
    {
        [NotNull] private readonly IGoalsSerializedDataConverter _goalsSerializedDataConverter;
        [NotNull] private readonly IParser _parser;

        public GoalsParser(
            [NotNull] IGoalsSerializedDataConverter goalsSerializedDataConverter,
            [NotNull] IParser parser)
        {
            ArgumentNullException.ThrowIfNull(goalsSerializedDataConverter);
            ArgumentNullException.ThrowIfNull(parser);

            _goalsSerializedDataConverter = goalsSerializedDataConverter;
            _parser = parser;
        }

        public string Serialize(IGoals goals)
        {
            GoalsSerializedData goalsSerializedData = _goalsSerializedDataConverter.From(goals);

            return _parser.Serialize(goalsSerializedData);
        }

        public IGoals Deserialize(string value)
        {
            GoalsSerializedData goalsSerializedData = _parser.Deserialize<GoalsSerializedData>(value);

            return _goalsSerializedDataConverter.To(goalsSerializedData);
        }
    }
}