using Game.Gameplay.Bag.Parsing;
using Game.Gameplay.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag.Composition
{
    public class BagComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<IBagPieceEntrySerializedDataConverter>(_ => new BagPieceEntrySerializedDataConverter()));

            ruleAdder.Add(
                ruleFactory.GetSingleton<IBagSerializedDataConverter>(r =>
                    new BagSerializedDataConverter(
                        r.Resolve<IBagPieceEntrySerializedDataConverter>(),
                        r.Resolve<IPieceGetter>()
                    )
                )
            );

            ruleAdder.Add(ruleFactory.GetSingleton<IBagContainer>(_ => new BagContainer()));
        }
    }
}