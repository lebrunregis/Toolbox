namespace Settings.Data
{
    public interface ISetting<T1, T2>
    {
        T1 Key { get; set; }
        T2 Value { get; set; }
    }
}
