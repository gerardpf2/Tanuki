using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.Unity
{
    // TODO: Test
    public class CameraGetter : ICameraGetter
    {
        public Camera GetMain()
        {
            Camera main = Camera.main;

            InvalidOperationException.ThrowIfNull(main);

            return main;
        }
    }
}