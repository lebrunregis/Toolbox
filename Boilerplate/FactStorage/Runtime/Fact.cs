
using System;
using Facts.Data;
using FactStorage.Data;
using SaveSystem.Runtime;

namespace Facts.Runtime
{
    public class Fact<T> : IFact<T>// where T : ISerializationCallbackReceiver
    {
        public T value;
       public FactPersistenceEnum Persistance { get ; set ; }
        public Type Type { get; }
        public FactScopeEnum Scope { get; set ; }
        public T DefaultValue { get; set ; }

        public Fact(T fact, FactPersistenceEnum persistence = FactPersistenceEnum.Runtime, FactScopeEnum scope = FactScopeEnum.Project)
        {
            value = fact;
            Persistance = persistence;
            Type = typeof(T);
            Scope = scope;
        }

        public T Value()
        {
            return value;
        }

        public void Value(T newValue)
        {
            value = newValue;
        }

        public void ResetValue()
        {
            value = DefaultValue;
        }
    }
}
