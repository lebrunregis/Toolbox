namespace Facts.Runtime
{
    public interface IFact<T>

    {
        T Value();
        void Value(T value);
        FactPersistence PersistanceState { get; }
    }
}