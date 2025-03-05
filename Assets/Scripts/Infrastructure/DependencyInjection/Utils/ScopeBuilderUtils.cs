using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Utils
{
    public static class ScopeBuilderUtils
    {
        public static Scope BuildRoot([NotNull] this IScopeBuilder scopeBuilder, IScopeComposer scopeComposer)
        {
            return scopeBuilder.Build(null, scopeComposer);
        }
    }
}