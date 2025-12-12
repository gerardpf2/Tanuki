namespace Infrastructure.Unity.Animator
{
    public interface IAnimationEventNotifier
    {
        void OnAnimationEnd(string animationName);
    }
}