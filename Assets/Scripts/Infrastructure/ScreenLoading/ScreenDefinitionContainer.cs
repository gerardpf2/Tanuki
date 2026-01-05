using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    [CreateAssetMenu(fileName = nameof(ScreenDefinitionContainer), menuName = "Tanuki/Infrastructure/ScreenLoading/" + nameof(ScreenDefinitionContainer))]
    public class ScreenDefinitionContainer : ScriptableObject, IScreenDefinitionGetter
    {
        [SerializeField] private Screen[] _screens;

        public IScreen Get(string key)
        {
            InvalidOperationException.ThrowIfNull(_screens);

            IScreen screen = null;

            foreach (IScreen screenCandidate in _screens)
            {
                InvalidOperationException.ThrowIfNull(screenCandidate);

                if (screenCandidate.Key != key)
                {
                    continue;
                }

                screen = screenCandidate;

                break;
            }

            InvalidOperationException.ThrowIfNullWithMessage(
                screen,
                $"Cannot get screen with Key: {key}"
            );

            return screen;
        }
    }
}