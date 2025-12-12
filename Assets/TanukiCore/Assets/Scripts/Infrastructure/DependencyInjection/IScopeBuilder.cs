namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuilder
    {
        PartialScope BuildPartial(Scope mainScope, IScopeComposer scopeComposer);

        Scope Build(Scope parentScope, IScopeComposer scopeComposer);
    }
}