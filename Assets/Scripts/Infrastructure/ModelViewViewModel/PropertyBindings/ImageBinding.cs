using Infrastructure.System.Exceptions;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class ImageBinding : PropertyBinding<Sprite>
    {
        [SerializeField] private Image _image;

        public override void Set(Sprite value)
        {
            InvalidOperationException.ThrowIfNull(_image);

            _image.sprite = value;
        }
    }
}