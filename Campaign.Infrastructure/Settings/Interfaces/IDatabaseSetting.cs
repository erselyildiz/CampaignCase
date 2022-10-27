namespace Campaign.Infrastructure.Settings.Interfaces
{
    public interface IDatabaseSetting
    {
        string ConnectionStrings { get; set; }
        string DatabaseName { get; set; }

    }
}