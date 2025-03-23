using UnityEngine;

namespace Infrastructure.Unity
{
    public class ShowInInspectorIfAttribute : PropertyAttribute
    {
        public readonly string TargetName;

        public ShowInInspectorIfAttribute(string targetName)
        {
            TargetName = targetName;
        }
    }
}