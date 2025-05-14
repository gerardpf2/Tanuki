using System;
using Game.Gameplay.View.Camera;
using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Board
{
    public class BoardViewModel : ViewModel, IDataSettable<BoardViewData>
    {
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _bottom;

        private ICameraBoardViewPropertiesSetter _cameraBoardViewPropertiesSetter;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] ICameraBoardViewPropertiesSetter cameraBoardViewPropertiesSetter)
        {
            ArgumentNullException.ThrowIfNull(cameraBoardViewPropertiesSetter);

            _cameraBoardViewPropertiesSetter = cameraBoardViewPropertiesSetter;
        }

        public void SetData([NotNull] BoardViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            Initialize(data.OnViewReady);
        }

        private void Initialize(Action onViewReady)
        {
            InvalidOperationException.ThrowIfNull(_top);
            InvalidOperationException.ThrowIfNull(_bottom);
            InvalidOperationException.ThrowIfNull(_cameraBoardViewPropertiesSetter);

            _cameraBoardViewPropertiesSetter.SetBoardViewLimits(_top.position.y, _bottom.position.y);

            onViewReady?.Invoke();
        }
    }
}