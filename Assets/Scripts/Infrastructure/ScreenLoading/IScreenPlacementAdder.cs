namespace Infrastructure.ScreenLoading
{
    public interface IScreenPlacementAdder
    {
        void Add(IScreenPlacement screenPlacement);

        void Remove(IScreenPlacement screenPlacement);
    }
}