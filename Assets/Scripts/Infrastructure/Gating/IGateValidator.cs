namespace Infrastructure.Gating
{
    public interface IGateValidator
    {
        bool Validate(string key);
    }
}