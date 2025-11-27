using Infrastructure.System.Exceptions;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class AnimatorSetBoolBinding : PropertyBinding<(string, bool)>
    {
        [SerializeField] private Animator _animator;

        public override void Set((string, bool) value)
        {
            InvalidOperationException.ThrowIfNull(_animator);

            _animator.SetBool(value.Item1, value.Item2);
        }
    }
}