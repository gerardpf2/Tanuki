using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleContainer
    {
        void Add<T>(IRule<T> rule, object key = null);

        bool TryGet<T>(out IRule<T> rule, object key = null);
    }
}