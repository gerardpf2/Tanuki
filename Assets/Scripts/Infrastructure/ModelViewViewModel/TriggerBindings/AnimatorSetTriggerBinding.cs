using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.TriggerBindings
{
    public class AnimatorSetTriggerBinding : TriggerBinding<string>
    {
        [SerializeField] private Animator _animator;

        public override void OnTriggered(string data)
        {
            InvalidOperationException.ThrowIfNull(_animator);

            _animator.SetTrigger(data);
        }
    }
}