using System;
using UnityEngine;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.ScreenLoading
{
    [Serializable]
    public class ScreenDefinition : IScreenDefinition
    {
        [SerializeField] private Screen _screen;

        public string Key => Screen.Key;

        public IScreen Screen
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_screen);

                return _screen;
            }
        }
    }
}