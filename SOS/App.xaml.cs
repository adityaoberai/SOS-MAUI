using SOS.Business;

namespace SOS;

public partial class App : Application
{
    public static SettingsRepository SettingsRepo { get; private set; }

    public static LocationService LocationService { get; private set; }

    public App(SettingsRepository repo, LocationService locationService)
    {
        InitializeComponent();

        MainPage = new AppShell();

        SettingsRepo = repo;

        LocationService = locationService;
    }
}
