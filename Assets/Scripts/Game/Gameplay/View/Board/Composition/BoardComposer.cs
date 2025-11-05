using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Board.Composition
{
    public class BoardComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardView>(r =>
                    new BoardView(
                        r.Resolve<IBoardContainer>(),
                        r.Resolve<IPieceViewDefinitionGetter>(),
                        r.Resolve<ILogger>(),
                        r.Resolve<IGameObjectPool>()
                    )
                )
            );
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<BoardViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<ICameraView>()
                    )
                )
            );
        }
    }
}