using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Unity.Animator
{
    public class NotifyAnimationEndOnStateExit : StateMachineBehaviour
    {
        [SerializeField] private string _animationName;

        private IAnimationEventNotifier _animationEventNotifier;

        public override void OnStateExit(
            [NotNull] UnityEngine.Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            ArgumentNullException.ThrowIfNull(animator);

            FindAnimationEventNotifierIfNeeded(animator);

            InvalidOperationException.ThrowIfNull(_animationEventNotifier);

            base.OnStateExit(animator, stateInfo, layerIndex);

            _animationEventNotifier.OnAnimationEnd(_animationName);
        }

        private void FindAnimationEventNotifierIfNeeded([NotNull] Component component)
        {
            ArgumentNullException.ThrowIfNull(component);

            _animationEventNotifier ??= component.GetComponent<IAnimationEventNotifier>();
        }
    }
}