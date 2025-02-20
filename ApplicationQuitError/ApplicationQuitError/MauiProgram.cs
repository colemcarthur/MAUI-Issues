using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using SCQuote.AppWindows;

namespace ApplicationQuitError;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(windowsLifecycleBuilder =>
                {
                    windowsLifecycleBuilder.OnWindowCreated(window =>
                    {
                        bool isClosing = false;

                        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);

                        appWindow.Closing += async (s, e) =>
                        {
                            e.Cancel = true;

                            if (isClosing)
                                return;

                            isClosing = true;

                            Window currentWindow = Application.Current.Windows[0];

                            if (currentWindow != null)
                            {
                                bool result = await currentWindow.Page.DisplayAlert(
                                    "Close App",
                                    "Are you sure you want to close this application?",
                                    "Yes",
                                    "Cancel");

                                if (result)
                                    Application.Current.Quit();
                            }

                            isClosing = false;
                        };

                    });
                });
#endif
            });

#if DEBUG
              builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<Modal>();

        return builder.Build();
    }
}
