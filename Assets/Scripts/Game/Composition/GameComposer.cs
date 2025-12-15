using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.View.UseCases;
using Game.REMOVE;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Composition
{
    public class GameComposer : ScopeComposer
    {
        [NotNull, ItemNotNull] private readonly IReadOnlyCollection<IGameScopeComposerBuilder> _childScopeComposerBuilders;

        public GameComposer([NotNull, ItemNotNull] IEnumerable<IGameScopeComposerBuilder> childScopeComposerBuilders)
        {
            ArgumentNullException.ThrowIfNull(childScopeComposerBuilders);

            List<IGameScopeComposerBuilder> childScopeComposerBuildersCopy = new();

            foreach (IGameScopeComposerBuilder gameScopeComposerBuilder in childScopeComposerBuilders)
            {
                ArgumentNullException.ThrowIfNull(gameScopeComposerBuilder);

                childScopeComposerBuildersCopy.Add(gameScopeComposerBuilder);
            }

            _childScopeComposerBuilders = childScopeComposerBuildersCopy;
        }

        protected override void AddSharedRules([NotNull] IRuleAdder ruleAdder, [NotNull] IRuleFactory ruleFactory)
        {
            ArgumentNullException.ThrowIfNull(ruleAdder);
            ArgumentNullException.ThrowIfNull(ruleFactory);

            base.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.Add(
                ruleFactory.GetInject<Load>((r, s) =>
                    s.Inject(
                        r.Resolve<ILoadGameplayUseCase>()
                    )
                )
            );
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base
                .GetChildScopeComposers()
                .Concat(_childScopeComposerBuilders.Select(childScopeComposerBuilder => childScopeComposerBuilder.Build()));
        }
    }
}