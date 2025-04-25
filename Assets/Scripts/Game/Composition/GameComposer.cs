using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Composition;
using Game.Gameplay.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Composition
{
    public class GameComposer : ScopeComposer
    {
        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new GameplayComposer());
        }

        protected override void Initialize([NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            base.Initialize(ruleResolver);

            ruleResolver.Resolve<ILoadGameplay>().Resolve(); // TODO
        }
    }
}