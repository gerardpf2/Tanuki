using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Game.Gameplay.Composition
{
    [CreateAssetMenu(fileName = nameof(GameplayComposerBuilder), menuName = "Tanuki/Game/Gameplay/Composition/" + nameof(GameplayComposerBuilder))]
    public class GameplayComposerBuilder : GameScopeComposerBuilder
    {
        [SerializeField] private GameplayDefinitionContainer _gameplayDefinitionContainer;
        [SerializeField] private PieceViewDefinitionContainer _pieceViewDefinitionContainer;

        public override IScopeComposer Build()
        {
            InvalidOperationException.ThrowIfNull(_gameplayDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_pieceViewDefinitionContainer);

            return new GameplayComposer(_gameplayDefinitionContainer, _pieceViewDefinitionContainer);
        }
    }
}