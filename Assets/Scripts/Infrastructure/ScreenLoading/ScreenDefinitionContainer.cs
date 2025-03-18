using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.ScreenLoading
{
    [CreateAssetMenu(fileName = nameof(ScreenDefinitionContainer), menuName = "Tanuki/Infrastructure/ScreenLoading/" + nameof(ScreenDefinitionContainer))]
    public class ScreenDefinitionContainer : ScriptableObject, IScreenDefinitionGetter
    {
        [NotNull] [SerializeField] private List<ScreenDefinition> _screenDefinitions = new();

        public IScreenDefinition Get(string key)
        {
            IScreenDefinition screenDefinition = _screenDefinitions.Find(screenDefinition => screenDefinition.Key == key);

            if (screenDefinition == null)
            {
                throw new InvalidOperationException($"Cannot get screen definition with Key: {key}");
            }

            return screenDefinition;
        }
    }
}