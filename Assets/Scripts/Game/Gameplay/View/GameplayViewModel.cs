using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View
{
    public class GameplayViewModel : ViewModel, IDataSettable<GameplayViewData>
    {
        private IGameplay _gameplay;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IGameplay gameplay)
        {
            ArgumentNullException.ThrowIfNull(gameplay);

            _gameplay = gameplay;
        }

        public void SetData([NotNull] GameplayViewData data)
        {
            ArgumentNullException.ThrowIfNull(data);
            InvalidOperationException.ThrowIfNull(_gameplay);

            _gameplay.Initialize(data.BoardId);
        }
    }
}