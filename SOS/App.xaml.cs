using SOS.Data;

namespace SOS;

public partial class App : Application
{
    public static SettingsRepository SettingsRepo { get; private set; }

    public App(SettingsRepository repo)
    {
        InitializeComponent();

        MainPage = new AppShell();

        SettingsRepo = repo;
    }
}
