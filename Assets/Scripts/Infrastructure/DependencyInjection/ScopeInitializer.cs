using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeInitializer : IScopeInitializer
    {
        public void Initialize([NotNull] Scope scope)
        {
            scope.Initialize?.Invoke(scope.RuleResolver);

            foreach (Scope partialScope in scope.PartialScopes)
            {
                Initialize(partialScope);
            }

            foreach (Scope childScope in scope.ChildScopes)
            {
                Initialize(childScope);
            }
        }
    }
}