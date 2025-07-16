using System.Collections.Generic;

namespace Settings.Data
{
    public interface ISettingsGroup
    {
        string Name { get; set; }
        Dictionary<string, ISettingTypeGetter> GetSettings { get; }
    }
}
