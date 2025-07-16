using System;

namespace Settings.Data
{
    public class SettingWrapper<T1, T2> : ISetting<T1, T2>, ISettingTypeGetter
    {
        public T1 Key { get => Key; set => Key = value; }
        public T2 Value { get => Value; set => SetValue(value); }
        public void SetValue(T2 value)
        {
            ValueType = value.GetType();
            Value = value;
        }

        public Type ValueType;
    }
}
