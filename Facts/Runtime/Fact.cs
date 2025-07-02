
namespace Facts.Runtime
{
    public class Fact<T> : IFact<T>
    {
        private FactPersistence persistanceState;
        public T value;

        public FactPersistence PersistanceState
        {
            get; set;
        }

        public Fact(T fact, FactPersistence persistence = FactPersistence.Runtime)
        {
            value = fact;
            persistanceState = persistence;
        }

        public T Value()
        {
            return value;

        }

        public void Value(T newValue)
        {
            value = newValue;
        }
    }
}
