using System.Collections.Generic;
using System.Linq;
using Game.GameplayEditor.Phases.Composition;
using Game.GameplayEditor.View.Composition;
using Infrastructure.DependencyInjection;

namespace Game.GameplayEditor.Composition
{
    public class GameplayEditorComposer : ScopeComposer
    {
        protected override IEnumerable<IScopeComposer> GetPartialScopeComposers()
        {
            return base.GetPartialScopeComposers().Append(new PhasesEditorComposer());
        }

        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new GameplayEditorViewComposer());
        }
    }
}