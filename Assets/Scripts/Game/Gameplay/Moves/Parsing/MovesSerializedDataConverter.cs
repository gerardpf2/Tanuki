using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Parsing
{
    public class MovesSerializedDataConverter : IMovesSerializedDataConverter
    {
        public IMoves To([NotNull] MovesSerializedData movesSerializedData)
        {
            ArgumentNullException.ThrowIfNull(movesSerializedData);

            return new Moves(movesSerializedData.Amount);
        }

        public MovesSerializedData From([NotNull] IMoves moves)
        {
            ArgumentNullException.ThrowIfNull(moves);

            return new MovesSerializedData { Amount = moves.Amount };
        }
    }
}