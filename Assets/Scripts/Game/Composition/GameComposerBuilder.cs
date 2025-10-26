using Game.Gameplay;
using Game.Gameplay.View.Pieces;
using Infrastructure.DependencyInjection;
using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Game.Composition
{
    [CreateAssetMenu(fileName = nameof(GameComposerBuilder), menuName = "Tanuki/Game/Composition/" + nameof(GameComposerBuilder))]
    public class GameComposerBuilder : GameScopeComposerBuilder
    {
        [SerializeField] private GameplayDefinitionContainer _gameplayDefinitionContainer;
        [SerializeField] private PieceViewDefinitionContainer _pieceViewDefinitionContainer;

        public override IScopeComposer Build()
        {
            InvalidOperationException.ThrowIfNull(_gameplayDefinitionContainer);
            InvalidOperationException.ThrowIfNull(_pieceViewDefinitionContainer);

            return new GameComposer(_gameplayDefinitionContainer, _pieceViewDefinitionContainer);
        }
    }
}