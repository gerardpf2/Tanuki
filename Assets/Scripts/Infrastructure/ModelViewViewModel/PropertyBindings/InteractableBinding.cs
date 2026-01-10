using Infrastructure.System.Exceptions;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class InteractableBinding : PropertyBinding<bool>
    {
        [SerializeField] private Selectable _selectable;

        public override void Set(bool value)
        {
            InvalidOperationException.ThrowIfNull(_selectable);

            _selectable.interactable = value;
        }
    }
}