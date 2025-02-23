namespace Infrastructure.DependencyInjection
{
    public interface IScopeComposer
    {
        void Compose(IScopeBuilderParametersSetter scopeBuilderParametersSetter);
    }
}