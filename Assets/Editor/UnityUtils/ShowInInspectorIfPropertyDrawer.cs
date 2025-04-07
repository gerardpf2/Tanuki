using Infrastructure.UnityUtils;
using UnityEditor;
using UnityEngine;

namespace Editor.UnityUtils
{
    [CustomPropertyDrawer(typeof(ShowInInspectorIfAttribute))]
    public class ShowInInspectorIfPropertyDrawer : PropertyDrawer
    {
        private bool _show;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_show)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // GetPropertyHeight called before OnGUI. Then, it can be cached in here
            RefreshShow(property);

            if (_show)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private void RefreshShow(SerializedProperty serializedProperty)
        {
            string targetPropertyPath = GetTargetPropertyPath(
                serializedProperty.propertyPath,
                serializedProperty.name,
                ((ShowInInspectorIfAttribute)attribute).TargetName
            );

            SerializedProperty targetProperty = serializedProperty.serializedObject.FindProperty(targetPropertyPath);

            _show = targetProperty.boolValue;
        }

        private static string GetTargetPropertyPath(string propertyPath, string name, string targetName)
        {
            // If List<T> is serialized, this may not work. Use ListWrapper<T> instead

            return $"{propertyPath.Remove(propertyPath.Length - name.Length)}{targetName}";
        }
    }
}