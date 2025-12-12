namespace Infrastructure.ModelViewViewModel
{
    public interface IDataSettable<in T>
    {
        void SetData(T data);
    }
}