using System.Collections.Generic;

namespace Settings.Data
{
    public class SettingsGroup : ISettingsGroup
    {
        public string Name { get; set; }

        Dictionary<string, ISettingTypeGetter> ISettingsGroup.Settings => Settings;

        public Dictionary<string, ISettingTypeGetter> Settings = new();
    }
}
