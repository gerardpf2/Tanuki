using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeInitializer : IScopeInitializer
    {
        public void Initialize([NotNull] Scope scope)
        {
            InitializeSingle(scope);

            foreach (PartialScope partialScope in scope.PartialScopes)
            {
                Initialize(partialScope);
            }

            foreach (Scope childScope in scope.ChildScopes)
            {
                Initialize(childScope);
            }
        }

        // TODO: Test
        public void Initialize([NotNull] PartialScope partialScope)
        {
            InitializeSingle(partialScope);
        }

        private static void InitializeSingle([NotNull] Scope scope)
        {
            scope.Initialize?.Invoke(scope.RuleResolver);
        }
    }
}