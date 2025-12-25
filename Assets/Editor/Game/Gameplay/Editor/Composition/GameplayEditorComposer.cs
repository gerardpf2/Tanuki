using System.Collections.Generic;
using System.Linq;
using Editor.Game.Gameplay.Editor.View.Composition;
using Infrastructure.DependencyInjection;

namespace Editor.Game.Gameplay.Editor.Composition
{
    public class GameplayEditorComposer : ScopeComposer
    {
        protected override IEnumerable<IScopeComposer> GetChildScopeComposers()
        {
            return base.GetChildScopeComposers().Append(new GameplayEditorViewComposer());
        }
    }
}