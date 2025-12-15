using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Parsing
{
    public class MovesSerializedDataConverter : IMovesSerializedDataConverter
    {
        public void To([NotNull] MovesSerializedData movesSerializedData, [NotNull] IMoves moves)
        {
            ArgumentNullException.ThrowIfNull(movesSerializedData);
            ArgumentNullException.ThrowIfNull(moves);

            moves.Amount = movesSerializedData.Amount;
        }

        public MovesSerializedData From([NotNull] IMoves moves)
        {
            ArgumentNullException.ThrowIfNull(moves);

            return new MovesSerializedData { Amount = moves.Amount };
        }
    }
}