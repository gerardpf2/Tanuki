using UnityEditor;
using UnityEngine;

namespace Editor.Unity.PropertyDrawers
{
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
            const string propertyName = "List";

            return property.FindPropertyRelative(propertyName);
        }
    }
}