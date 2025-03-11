namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public interface IPropertyBinding<in T>
    {
        string Key { get; }

        void Set(T value);
    }
}