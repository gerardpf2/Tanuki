namespace Infrastructure.DependencyInjection
{
    public interface IEnabledGateKeyGetter
    {
        bool Contains(object gateKey);
    }
}