using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.Goals;
using Game.Gameplay.View.Moves;
using Game.Gameplay.View.Player;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Actions.Composition
{
    public class ActionsComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IActionFactory>(r =>
                    new ActionFactory(
                        r.Resolve<IMovementHelper>(),
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>(),
                        r.Resolve<IGoalsView>(),
                        r.Resolve<IMovesView>(),
                        r.Resolve<IPlayerPieceGhostView>(),
                        r.Resolve<IPlayerPieceView>(),
                        r.Resolve<ICoroutineRunner>()
                    )
                )
            );
        }
    }
}