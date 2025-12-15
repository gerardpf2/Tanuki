using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Animation.Animator
{
    public class AnimatorTriggerNameContainer : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Animator _animator;
        [NotNull, SerializeField, HideInInspector] private List<string> _triggerNames = new();

        public bool Contains(string triggerName)
        {
            return _triggerNames.Contains(triggerName);
        }

        [ContextMenu(nameof(Refresh))]
        private void Refresh()
        {
            InvalidOperationException.ThrowIfNull(_animator);
            InvalidOperationException.ThrowIfNull(_animator.runtimeAnimatorController);

            _triggerNames.Clear();

            foreach (AnimatorControllerParameter animatorControllerParameter in _animator.parameters)
            {
                InvalidOperationException.ThrowIfNull(animatorControllerParameter);

                if (animatorControllerParameter.type is not AnimatorControllerParameterType.Trigger)
                {
                    continue;
                }

                _triggerNames.Add(animatorControllerParameter.name);
            }

#if UNITY_EDITOR

            UnityEditor.EditorUtility.SetDirty(this);

#endif
        }
    }
}