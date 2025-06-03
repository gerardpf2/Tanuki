using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = nameof(GameplayDefinitionContainer), menuName = "Tanuki/Game/Gameplay/" + nameof(GameplayDefinitionContainer))]
    public class GameplayDefinitionContainer : ScriptableObject, IGameplayDefinitionGetter
    {
        [NotNull, ItemNotNull, SerializeField] private List<GameplayDefinition> _gameplayDefinitions = new();

        public IGameplayDefinition Get(string id)
        {
            IGameplayDefinition gameplayDefinition = _gameplayDefinitions.Find(gameplayDefinition => gameplayDefinition.Id == id);

            InvalidOperationException.ThrowIfNullWithMessage(
                gameplayDefinition,
                $"Cannot get gameplay definition with Id: {id}"
            );

            return gameplayDefinition;
        }
    }
}