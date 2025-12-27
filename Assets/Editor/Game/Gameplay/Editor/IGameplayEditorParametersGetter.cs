namespace Editor.Game.Gameplay.Editor
{
    public interface IGameplayEditorParametersGetter
    {
        int MinColumns { get; }

        int MaxColumns { get; }

        int InitialColumns { get; }
    }
}