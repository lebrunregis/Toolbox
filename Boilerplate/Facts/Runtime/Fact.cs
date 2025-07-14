
using System;
using Facts.Data;

namespace Facts.Runtime
{
    public class Fact<T> : IFact<T>// where T : ISerializationCallbackReceiver
    {
        private FactPersistenceEnum persistanceState;
        public T value;
        private readonly Type type;
        public FactPersistenceEnum PersistanceState { get => persistanceState; set => persistanceState = value; }
        public Type Type { get => type; }

        public Fact(T fact, FactPersistenceEnum persistence = FactPersistenceEnum.Runtime)
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
