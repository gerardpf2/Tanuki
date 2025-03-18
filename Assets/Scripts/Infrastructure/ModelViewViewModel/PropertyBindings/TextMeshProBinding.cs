using Infrastructure.System.Exceptions;
using TMPro;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class TextMeshProBinding : PropertyBinding<string>
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        public override void Set(string value)
        {
            InvalidOperationException.ThrowIfNull(_textMeshProUGUI);

            _textMeshProUGUI.text = value;
        }
    }
}