using System.Collections.Generic;

namespace Settings.Data
{
    public class SettingsGroup : ISettingsGroup
    {
        public string Name { get; set; }

        public Dictionary<string, ISettingTypeGetter> GetSettings => throw new System.NotImplementedException();

        public Dictionary<string, ISettingTypeGetter> Settings = new();
    }
}
