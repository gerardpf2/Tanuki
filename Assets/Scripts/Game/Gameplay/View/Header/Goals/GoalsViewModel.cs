using Infrastructure.DependencyInjection;
using Infrastructure.ModelViewViewModel;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Header.Goals
{
    public class GoalsViewModel : ViewModel, IDataSettable<GoalsViewData>
    {
        private IGoalsViewContainer _goalsViewContainer;

        protected override void Awake()
        {
            base.Awake();

            InjectResolver.Resolve(this);
        }

        public void Inject([NotNull] IGoalsViewContainer goalsViewContainer)
        {
            ArgumentNullException.ThrowIfNull(goalsViewContainer);

            _goalsViewContainer = goalsViewContainer;
        }

        public void SetData(GoalsViewData _) { }
    }
}