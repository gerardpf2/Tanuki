using Game.Gameplay.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Camera.Composition
{
    public class CameraComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<ICamera>(_ => new Camera()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<ICameraRowsUpdater>(r =>
                    new CameraRowsUpdater(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<ICamera>()
                    )
                )
            );
        }
    }
}