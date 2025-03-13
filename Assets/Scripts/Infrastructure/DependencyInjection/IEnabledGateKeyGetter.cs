namespace Infrastructure.DependencyInjection
{
    public interface IEnabledGateKeyGetter
    {
        bool Contains(string gateKey);
    }
}