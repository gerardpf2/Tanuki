using TMPro;
using UnityEngine;

namespace Infrastructure.ModelViewViewModel.PropertyBindings
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProBinding : PropertyBinding<string>
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        public override void Set(string value)
        {
            _textMeshProUGUI.text = value;
        }

        private void Awake()
        {
            GetComponentIfNeeded();
        }

        private void Reset()
        {
            GetComponentIfNeeded();
        }

        private void GetComponentIfNeeded()
        {
            if (_textMeshProUGUI != null)
            {
                return;
            }

            _textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
}