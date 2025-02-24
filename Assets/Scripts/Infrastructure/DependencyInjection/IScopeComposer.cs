namespace Infrastructure.DependencyInjection
{
    public interface IScopeComposer
    {
        void Compose(ScopeBuildingContext scopeBuildingContext);
    }
}