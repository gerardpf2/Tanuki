namespace Infrastructure.DependencyInjection
{
    public interface IScopeInitializer
    {
        void Initialize(Scope scope);
    }
}