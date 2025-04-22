using UnityEngine;

namespace Infrastructure.UnityUtils
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