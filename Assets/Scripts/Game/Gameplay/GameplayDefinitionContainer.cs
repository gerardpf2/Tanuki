using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = nameof(GameplayDefinitionContainer), menuName = "Tanuki/Game/Gameplay/" + nameof(GameplayDefinitionContainer))]
    public class GameplayDefinitionContainer : ScriptableObject, IGameplayDefinitionGetter
    {
        [SerializeField] private GameplayDefinition[] _gameplayDefinitions;

        public IGameplayDefinition Get(string id)
        {
            InvalidOperationException.ThrowIfNull(_gameplayDefinitions);

            IGameplayDefinition gameplayDefinition = null;

            foreach (GameplayDefinition gameplayDefinitionCandidate in _gameplayDefinitions)
            {
                InvalidOperationException.ThrowIfNull(gameplayDefinitionCandidate);

                if (gameplayDefinitionCandidate.Id != id)
                {
                    continue;
                }

                gameplayDefinition = gameplayDefinitionCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                gameplayDefinition,
                $"Cannot get gameplay definition with Id: {id}"
            );

            return gameplayDefinition;
        }
    }
}