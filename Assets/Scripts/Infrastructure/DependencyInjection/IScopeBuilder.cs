namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuilder
    {
        Scope Build(IScopeComposer scopeComposer, Scope parentScope);
    }
}