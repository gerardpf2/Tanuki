namespace Infrastructure.DependencyInjection
{
    public interface IScopeInitializer
    {
        void Initialize(PartialScope partialScope);

        void Initialize(Scope scope);
    }
}