using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleGetter
    {
        [ContractAnnotation("=> true, rule:notnull; => false, rule:null")]
        bool TryGet<T>(out IRule<T> rule, object key = null);
    }
}