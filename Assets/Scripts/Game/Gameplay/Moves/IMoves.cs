using JetBrains.Annotations;

namespace Game.Gameplay.Moves
{
    public interface IMoves
    {
        int Amount { get; set; }

        [NotNull]
        IMoves Clone();
    }
}