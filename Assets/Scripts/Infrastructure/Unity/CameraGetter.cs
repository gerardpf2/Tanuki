using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.Unity
{
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