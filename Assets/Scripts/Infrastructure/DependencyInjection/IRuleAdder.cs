using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleAdder
    {
        void Add<T>(IRule<T> rule, object key = null);
    }
}