using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeInitializer : IScopeInitializer
    {
        public void Initialize([NotNull] Scope scope)
        {
            InitializeSingle(scope);

            foreach (Scope partialScope in scope.PartialScopes)
            {
                InitializeSingle(partialScope);
            }

            foreach (Scope childScope in scope.ChildScopes)
            {
                Initialize(childScope);
            }
        }

        private static void InitializeSingle([NotNull] Scope scope)
        {
            scope.Initialize?.Invoke(scope.RuleResolver);
        }
    }
}