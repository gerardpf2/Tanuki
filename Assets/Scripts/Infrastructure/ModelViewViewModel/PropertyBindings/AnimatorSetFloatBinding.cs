using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class AnimatorSetFloatBinding : PropertyBinding<float>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _name;

        public override void Set(float value)
        {
            InvalidOperationException.ThrowIfNull(_animator);

            _animator.SetFloat(_name, value);
        }
    }
}