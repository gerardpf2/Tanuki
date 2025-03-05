namespace Infrastructure.DependencyInjection
{
    public interface IScopeInitializer
    {
        void Initialize(Scope scope);

        void Initialize(PartialScope partialScope);
    }
}