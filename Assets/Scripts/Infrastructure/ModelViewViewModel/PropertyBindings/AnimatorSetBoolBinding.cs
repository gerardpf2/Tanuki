using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class AnimatorSetBoolBinding : PropertyBinding<bool>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _name;

        public override void Set(bool value)
        {
            InvalidOperationException.ThrowIfNull(_animator);

            _animator.SetBool(_name, value);
        }
    }
}