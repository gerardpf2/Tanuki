using Game.Gameplay.View.Common.Pieces;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalViewModel : ViewModel, IDataSettable<GoalViewData>
    {
        [SerializeField] private PieceSpriteContainer _pieceSpriteContainer;

        [NotNull] private readonly IBoundProperty<Sprite> _sprite = new BoundProperty<Sprite>("Sprite");
        [NotNull] private readonly IBoundProperty<string> _initialAmount = new BoundProperty<string>("InitialAmount");
        [NotNull] private readonly IBoundProperty<string> _currentAmount = new BoundProperty<string>("CurrentAmount");

        protected override void Awake()
        {
            base.Awake();

            Add(_sprite);
            Add(_initialAmount);
            Add(_currentAmount);
        }

        public void SetData([NotNull] GoalViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);
            InvalidOperationException.ThrowIfNull(_pieceSpriteContainer);

            _sprite.Value = _pieceSpriteContainer.Get(data.PieceType);
            _initialAmount.Value = data.InitialAmount.ToString();
            _currentAmount.Value = data.CurrentAmount.ToString();
        }
    }
}