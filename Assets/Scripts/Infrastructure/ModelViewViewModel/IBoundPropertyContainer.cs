namespace Infrastructure.ModelViewViewModel
{
    public interface IBoundPropertyContainer
    {
        void Add<T>(IBoundProperty<T> boundProperty);

        IBoundProperty<T> Get<T>(string key);
    }
}