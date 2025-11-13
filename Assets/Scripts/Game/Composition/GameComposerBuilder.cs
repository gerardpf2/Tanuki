using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Game.Composition
{
    [CreateAssetMenu(fileName = nameof(GameComposerBuilder), menuName = "Tanuki/Game/Composition/" + nameof(GameComposerBuilder))]
    public class GameComposerBuilder : GameScopeComposerBuilder
    {
        [SerializeField] private GameScopeComposerBuilder[] _childScopeComposerBuilders;

        public override IScopeComposer Build()
        {
            InvalidOperationException.ThrowIfNull(_childScopeComposerBuilders);

            foreach (GameScopeComposerBuilder gameScopeComposerBuilder in _childScopeComposerBuilders)
            {
                InvalidOperationException.ThrowIfNull(gameScopeComposerBuilder);
            }

            return new GameComposer(_childScopeComposerBuilders);
        }
    }
}