using Game.Gameplay.Board.Parsing;
using Game.Gameplay.Pieces.Parsing;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Composition
{
    public class BoardComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBoardSerializedDataConverter>(r =>
                    new BoardSerializedDataConverter(
                        r.Resolve<IPiecePlacementSerializedDataConverter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IBoard>(_ => new Board()));
        }
    }
}