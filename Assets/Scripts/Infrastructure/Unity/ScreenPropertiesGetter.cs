using UnityEngine;

namespace Infrastructure.Unity
{
    // TODO: Test
    public class ScreenPropertiesGetter : IScreenPropertiesGetter
    {
        public int Width => Screen.width;

        public int Height => Screen.height;
    }
}