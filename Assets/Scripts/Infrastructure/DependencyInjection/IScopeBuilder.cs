namespace Infrastructure.DependencyInjection
{
    public interface IScopeBuilder
    {
        Scope BuildAsChildOf(Scope scope, IScopeComposer scopeComposer);

        Scope BuildAsPartialOf(Scope scope, IScopeComposer scopeComposer);
    }
}