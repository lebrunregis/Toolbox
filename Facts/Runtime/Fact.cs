
using System;

namespace Facts.Runtime
{
    public class Fact<T> : IFact<T>// where T : ISerializationCallbackReceiver
    {
        private FactPersistence persistanceState;
        public T value;
        private System.Type type;
        public FactPersistence PersistanceState { get => persistanceState; set => persistanceState = value; }
        public Type Type { get => type; }

        public Fact(T fact, FactPersistence persistence = FactPersistence.Runtime)
        {
            value = fact;
            PersistanceState = persistence;
            type = typeof(T);
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
