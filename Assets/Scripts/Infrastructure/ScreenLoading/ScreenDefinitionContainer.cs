using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    [CreateAssetMenu(fileName = nameof(ScreenDefinitionContainer), menuName = "Tanuki/Infrastructure/ScreenLoading/" + nameof(ScreenDefinitionContainer))]
    public class ScreenDefinitionContainer : ScriptableObject, IScreenDefinitionGetter
    {
        [SerializeField] private ScreenDefinition[] _screenDefinitions;

        public IScreenDefinition Get(string key)
        {
            InvalidOperationException.ThrowIfNull(_screenDefinitions);

            IScreenDefinition screenDefinition = null;

            foreach (ScreenDefinition screenDefinitionCandidate in _screenDefinitions)
            {
                InvalidOperationException.ThrowIfNull(screenDefinitionCandidate);

                if (screenDefinitionCandidate.Key != key)
                {
                    continue;
                }

                screenDefinition = screenDefinitionCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                screenDefinition,
                $"Cannot get screen definition with Key: {key}"
            );

            return screenDefinition;
        }
    }
}