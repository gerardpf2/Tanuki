namespace Infrastructure.DependencyInjection
{
    public interface IEnabledGateKeyContainer
    {
        void Add(object gateKey);

        bool Contains(object gateKey);
    }
}