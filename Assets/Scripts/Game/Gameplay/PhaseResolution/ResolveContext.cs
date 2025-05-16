namespace Game.Gameplay.PhaseResolution
{
    public class ResolveContext
    {
        public readonly int? Column;

        public ResolveContext(int? column)
        {
            Column = column;
        }
    }
}