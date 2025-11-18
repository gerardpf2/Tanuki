using Game.Gameplay.Board;
using Game.Gameplay.View.Camera;
using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging;
using Infrastructure.System.Exceptions;
using Infrastructure.Unity;
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

            ruleAdder.Add(ruleFactory.GetSingleton<IBoard>(_ => new Gameplay.Board.Board()), "View");

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardView>(r =>
                    new BoardView(
                        r.Resolve<IBoard>(),
                        r.Resolve<IBoard>("View"),
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
                ruleFactory.GetInject<BoardGroundViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IBoard>("View"),
                        r.Resolve<ICameraView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<BoardViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IBoardView>(),
                        r.Resolve<ICameraView>()
                    )
                )
            );

            ruleAdder.Add(
                ruleFactory.GetInject<BoardWallsViewModel>((r, vm) =>
                    vm.Inject(
                        r.Resolve<IBoard>("View"),
                        r.Resolve<ICameraView>(),
                        r.Resolve<ICameraGetter>()
                    )
                )
            );
        }
    }
}