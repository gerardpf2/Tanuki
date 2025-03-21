using Infrastructure.Unity;
using UnityEditor;
using UnityEngine;

namespace Editor.Unity.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ListWrapper), true)]
    public class ListWrapperPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, GetListProperty(property), label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(GetListProperty(property), true);
        }

        private static SerializedProperty GetListProperty(SerializedProperty property)
        {
            const string propertyName = "_list";

            return property.FindPropertyRelative(propertyName);
        }
    }
}