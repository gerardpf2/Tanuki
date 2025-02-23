namespace Infrastructure.DependencyInjection
{
    public interface IScopeConstructor
    {
        Scope Construct(IScopeComposer scopeComposer, Scope parentScope);
    }
}