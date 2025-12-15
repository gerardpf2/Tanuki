using Game.Gameplay.Board;
using Game.Gameplay.Camera;
using Game.Gameplay.View.Board.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Camera.Composition
{
    public class CameraComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<ICameraView>(r =>
                    new CameraView(
                        r.Resolve<IBoard>(BoardComposerKeys.Board.View),
                        r.Resolve<ICamera>(),
                        r.Resolve<ICameraGetter>()
                    )
                )
            );
        }
    }
}