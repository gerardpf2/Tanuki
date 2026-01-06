using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.ScreenLoading
{
    // TODO: Test
    public class ScreenStack : IScreenStack
    {
        [NotNull, ItemNotNull] private readonly LinkedList<IScreen> _screens = new(); // ItemNotNull as long as all Add check for null

        private IScreen FocusedScreen => _screens.Last?.Value;

        public void Push([NotNull] IScreen screen)
        {
            ArgumentNullException.ThrowIfNull(screen);

            IScreen focusedScreen = FocusedScreen;

            if (focusedScreen == screen)
            {
                return;
            }

            AddLast(screen);

            focusedScreen?.OnFocus(false);
            screen.OnFocus(true);
        }

        public void Remove([NotNull] IScreen screen)
        {
            ArgumentNullException.ThrowIfNull(screen);

            IScreen focusedScreen = FocusedScreen;

            if (focusedScreen == screen)
            {
                screen.OnFocus(false);

                _screens.RemoveLast();
            }
            else if (!TryRemove(screen))
            {
                InvalidOperationException.Throw($"Cannot remove screen with Key: {screen.Key}");
            }

            IScreen newFocusedScreen = FocusedScreen;

            newFocusedScreen?.OnFocus(true);
        }

        private bool TryRemove(IScreen screen)
        {
            return _screens.Remove(screen);
        }

        private void AddLast([NotNull] IScreen screen)
        {
            ArgumentNullException.ThrowIfNull(screen);

            TryRemove(screen);

            _screens.AddLast(screen);
        }
    }
}