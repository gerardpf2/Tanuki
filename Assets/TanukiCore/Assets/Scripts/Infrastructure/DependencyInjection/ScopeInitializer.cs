using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeInitializer : IScopeInitializer
    {
        public void Initialize([NotNull] PartialScope partialScope)
        {
            ArgumentNullException.ThrowIfNull(partialScope);

            InitializeSingle(partialScope);
        }

        public void Initialize([NotNull] Scope scope)
        {
            ArgumentNullException.ThrowIfNull(scope);

            InitializeSingle(scope);

            foreach (PartialScope partialScope in scope.GetPartialScopes())
            {
                Initialize(partialScope);
            }

            foreach (Scope childScope in scope.GetChildScopes())
            {
                Initialize(childScope);
            }
        }

        private static void InitializeSingle([NotNull] Scope scope)
        {
            ArgumentNullException.ThrowIfNull(scope);

            scope.Initialize?.Invoke(scope.RuleResolver);
        }
    }
}