using System.Collections.Generic;

namespace Settings.Data
{
    public interface ISettings
    {
        Dictionary<int, ISettingsGroup> GetSettings();
    }
}
