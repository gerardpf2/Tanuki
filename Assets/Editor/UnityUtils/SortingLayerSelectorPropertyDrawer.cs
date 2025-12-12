using System;
using System.Linq;
using Infrastructure.UnityUtils;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Editor.UnityUtils
{
    [CustomPropertyDrawer(typeof(SortingLayerSelectorAttribute))]
    public class SortingLayerSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, GUIContent label)
        {
            ArgumentNullException.ThrowIfNull(property);

            string[] names = SortingLayer.layers.Select(layer => layer.name).ToArray();

            int index = Math.Max(Array.IndexOf(names, property.stringValue), 0);

            index = EditorGUI.Popup(position, label.text, index, names);

            property.stringValue = names[index];
        }
    }
}