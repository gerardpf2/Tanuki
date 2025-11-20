namespace Infrastructure.ModelViewViewModel.TriggerBindings
{
    public interface ITriggerBinding<in T>
    {
        string Key { get; }

        void OnTriggered(T data);
    }
}