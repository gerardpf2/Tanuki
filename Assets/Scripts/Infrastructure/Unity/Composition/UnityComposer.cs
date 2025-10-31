using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Unity.Composition
{
    public class UnityComposer : ScopeComposer
    {
        [NotNull] private readonly ICoroutineRunner _coroutineRunner;

        public UnityComposer([NotNull] ICoroutineRunner coroutineRunner)
        {
            ArgumentNullException.ThrowIfNull(coroutineRunner);

            _coroutineRunner = coroutineRunner;
        }

        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<ICameraGetter>(_ => new CameraGetter()));

            ruleAdder.Add(ruleFactory.GetInstance(_coroutineRunner));

            ruleAdder.Add(ruleFactory.GetSingleton<IDeltaTimeGetter>(_ => new DeltaTimeGetter()));

            ruleAdder.Add(ruleFactory.GetSingleton<IScreenPropertiesGetter>(_ => new ScreenPropertiesGetter()));
        }
    }
}