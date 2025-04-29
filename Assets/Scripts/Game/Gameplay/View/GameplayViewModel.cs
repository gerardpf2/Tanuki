using Game.Gameplay.View.Board;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View
{
    public class GameplayViewModel : ViewModel, IDataSettable<GameplayViewData>
    {
        [SerializeField] private BoardViewModel _boardViewModel; // TODO: Remove. Resolve using bindings

        [NotNull] private readonly IBoundProperty<BoardViewData> _boardViewData = new BoundProperty<BoardViewData>("BoardViewData", null);

        protected override void Awake()
        {
            base.Awake();

            Add(_boardViewData);
        }

        public void SetData([NotNull] GameplayViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _boardViewModel.SetData(data.BoardViewData); // TODO: Remove. Resolve using bindings

            _boardViewData.Value = data.BoardViewData;
        }
    }
}