using Facts.Data;
using FactStorage.Data;

namespace SaveSystem.Runtime
{
    public interface IFact<T>

    {
        T Value();
        void Value(T value);
        FactPersistenceEnum Persistance { get; }
        FactScopeEnum Scope { get; }
    }
}