using Facts.Data;

namespace Facts.Runtime
{
    public interface IFact<T>

    {
        T Value();
        void Value(T value);
        FactPersistenceEnum PersistanceState { get; }
    }
}