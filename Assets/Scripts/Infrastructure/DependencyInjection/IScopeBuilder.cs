namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuilder
    {
        Scope Build(Scope parentScope, IScopeComposer scopeComposer);

        PartialScope BuildPartial(Scope mainScope, IScopeComposer scopeComposer);
    }
}