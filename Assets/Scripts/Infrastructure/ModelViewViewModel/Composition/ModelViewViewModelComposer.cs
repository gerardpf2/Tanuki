using Infrastructure.DependencyInjection;
using JetBrains.Annotations;

namespace Infrastructure.ModelViewViewModel.Composition
{
    public class ModelViewViewModelComposer : ScopeComposer
    {
        protected override void AddPublicRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddPublicRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetTransient<IBoundPropertyContainer>(_ => new BoundPropertyContainer()));

            ruleAdder.Add(ruleFactory.GetTransient<IBoundMethodContainer>(_ => new BoundMethodContainer()));
        }

        protected override void AddGlobalRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            base.AddGlobalRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<ViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IBoundPropertyContainer>(),
                        r.Resolve<IBoundMethodContainer>()
                    )
                )
            );
        }
    }
}