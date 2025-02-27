using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleGetter
    {
        bool TryGet<T>(out IRule<T> rule, object key = null);
    }
}