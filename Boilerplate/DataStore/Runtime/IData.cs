using DataStore.Runtime;

namespace SaveSystem.Runtime
{
    public interface IData<T> : IDataType
    {
        T Value { get; set; }
    }
}