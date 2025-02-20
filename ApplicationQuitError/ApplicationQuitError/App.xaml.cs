using SCQuote.AppWindows;

namespace ApplicationQuitError;

public partial class App : Application
{
    private readonly MainWindow mainWindow;

    private IServiceProvider serviceProvider;

    public App(MainWindow mainWindow)
    {
        this.mainWindow = mainWindow;

        InitializeComponent();
    }

    private static ServiceProvider ConfigureServiceProviders()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<AppShell>()
            .BuildServiceProvider();

        return serviceProvider;
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        // this is to allow us to pass the Secure Storage Service to the getNewTokenTest and the appShell
        serviceProvider = ConfigureServiceProviders();

        AppShell appShell = serviceProvider.GetRequiredService<AppShell>();

        mainWindow.Page = appShell;

        const double minWidth = 980;
        const double minHeight = 980;

        const double maxWidth = 1920;
        const double maxHeight = 1080;

        mainWindow.Width = minWidth;
        mainWindow.Height = minHeight;

        mainWindow.MinimumWidth = minWidth;
        mainWindow.MinimumHeight = minHeight;

        mainWindow.MaximumWidth = maxWidth;
        mainWindow.MaximumHeight = maxHeight;

        mainWindow.Y = 0;
        mainWindow.X = 0;

        return mainWindow;
    }
}