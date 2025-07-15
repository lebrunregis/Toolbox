namespace SaveSystem.Runtime
{
    public interface ISaveSerializer
    {
        void Serialize(string path, SaveFile file);
        void Deserialize(out SaveFile file, SaveSlot slot, string stateId);
    }
}
