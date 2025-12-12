using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class AnimatorSetIntegerBinding : PropertyBinding<int>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _name;

        public override void Set(int value)
        {
            InvalidOperationException.ThrowIfNull(_animator);

            _animator.SetInteger(_name, value);
        }
    }
}