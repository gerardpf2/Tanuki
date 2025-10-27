using Game.Gameplay.Board;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging;
using Infrastructure.System.Exceptions;
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
                        r.Resolve<ILogger>()
                    )
                )
            );
        }
    }
}