namespace Infrastructure.DependencyInjection
{
    public interface IScopeComposer
    {
        void Compose(IScopeBuildingContext scopeBuildingContext);
    }
}