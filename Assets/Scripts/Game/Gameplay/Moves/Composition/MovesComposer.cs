using Game.Gameplay.Moves.Parsing;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Moves.Composition
{
    public class MovesComposer : ScopeComposer
    {
        protected override void AddRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddRules(ruleAdder, ruleFactory);

            ruleAdder.Add(ruleFactory.GetSingleton<IMovesSerializedDataConverter>(_ => new MovesSerializedDataConverter()));

            ruleAdder.Add(ruleFactory.GetSingleton<IMoves>(_ => new Moves()));
        }
    }
}