namespace SaveSystem.Runtime
{
    public interface ISaveSystem
    {
        SaveFile Save(GameFactsStorage gamestate);
        void Load(SaveFile file);
    }

}