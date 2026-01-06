namespace Infrastructure.ScreenLoading
{
    public interface IScreenStack
    {
        void Push(IScreen screen);

        void Remove(IScreen screen);
    }
}