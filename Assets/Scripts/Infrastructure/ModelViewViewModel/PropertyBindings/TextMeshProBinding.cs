using TMPro;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    public class TextMeshProBinding : PropertyBinding<string>
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        public override void Set(string value)
        {
            _textMeshProUGUI.text = value;
        }
    }
}