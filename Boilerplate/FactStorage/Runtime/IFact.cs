using Facts.Data;

namespace SaveSystem.Runtime
{
    public interface IFact<T>

    {
        T Value();
        void Value(T value);
        FactPersistenceEnum PersistanceState { get; }
    }
}