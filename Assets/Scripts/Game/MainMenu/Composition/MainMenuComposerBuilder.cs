using Infrastructure.DependencyInjection;
using UnityEngine;

namespace Game.MainMenu.Composition
{
    [CreateAssetMenu(fileName = nameof(MainMenuComposerBuilder), menuName = "Tanuki/Game/MainMenu/Composition/" + nameof(MainMenuComposerBuilder))]
    public class MainMenuComposerBuilder : GameScopeComposerBuilder
    {
        public override IScopeComposer Build()
        {
            return new MainMenuComposer();
        }
    }
}