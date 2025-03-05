namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuilder
    {
        Scope Build(Scope parentScope, IScopeComposer scopeComposer);

        Scope BuildPartial(Scope mainScope, IScopeComposer scopeComposer);
    }
}