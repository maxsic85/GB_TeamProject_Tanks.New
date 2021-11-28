namespace AS
{
    public interface IDataSave<T>
    {
        void Save(T data, string path = null);
        T Load(string path = null);

    }
}