using Campaign.Infrastructure.Settings.Interfaces;

namespace Campaign.Infrastructure.Settings
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
    }
}