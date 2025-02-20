using CommunityToolkit.Maui.Views;

namespace ApplicationQuitError.Controls;

public partial class BasePopUpPage : ContentPage
{
    //private bool settingPageSizeOptions;

    public static readonly BindableProperty IsPageBusyProperty =
        BindableProperty.Create(
            nameof(IsPageBusy),
            typeof(bool),
            typeof(BasePopUpPage),
            false,
            BindingMode.TwoWay);

    public bool IsPageBusy
    {
        get => (bool)GetValue(IsPageBusyProperty);
        set => SetValue(IsPageBusyProperty, value);
    }

    public static readonly BindableProperty PopupWidthProperty =
        BindableProperty.Create(
            nameof(PopupWidth),
            typeof(double),
            typeof(BasePopUpPage),
            500.0,
            BindingMode.TwoWay);

    public double PopupWidth
    {
        get => (double)GetValue(PopupWidthProperty);
        set => SetValue(PopupWidthProperty, value);
    }

    public static readonly BindableProperty PopupHeightProperty =
        BindableProperty.Create(
            nameof(PopupHeight),
            typeof(double),
            typeof(BasePopUpPage),
            500.0,
            BindingMode.TwoWay);

    public double PopupHeight
    {
        get => (double)GetValue(PopupHeightProperty);
        set => SetValue(PopupHeightProperty, value);
    }

    public BasePopUpPage()
    {
        InitializeComponent();

        Loaded += PageLoaded;

        SemanticOrderView tabOrderControl = (SemanticOrderView)GetTemplateChild("tabOrderControl");
        Border popupContainer = (Border)GetTemplateChild("popupContainer");

        tabOrderControl.ViewOrder = new List<View>() { popupContainer };
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        //        if (settingPageSizeOptions)
        //            return;

        //        settingPageSizeOptions = true;

        //#if WINDOWS || MACCATALYST
        //        PopupWidth = width;
        //#else
        //        PopupWidth = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) - 60;
        //#endif

        //        settingPageSizeOptions = false;
    }

    private async void PageLoaded(object sender, EventArgs e)
    {
        Loaded -= PageLoaded;

        await PoppingIn();
    }

    public async Task PopModalAsync()
    {
        await PoppingOut();

        await Navigation.PopModalAsync(animated: false);
    }

    public async Task GoToAsync(string route, Dictionary<string, object> data)
    {
        await PoppingOut();

        await Shell.Current.GoToAsync(route, data);
    }

    public async Task GoToAsync(string route)
    {
        await PoppingOut();

        await Shell.Current.GoToAsync(route);
    }

    private Task PoppingIn()
    {
        Border popupContainer = (Border)GetTemplateChild("popupContainer");

        var done = new TaskCompletionSource();

        // Measure the actual content size
        Size contentSize = popupContainer.Measure(Width, Height);
        double contentHeight = contentSize.Height;

        // Start by translating the content below / off screen
        popupContainer.TranslationY = contentHeight;

        // Animate the translucent background, fading into view
        this.Animate("Background",
            callback: v => Background = new SolidColorBrush(Colors.Black.WithAlpha((float)v)),
            start: 0d,
            end: 0.3d,
            rate: 32,
            length: 350,
            easing: Easing.CubicOut,
            finished: (v, k) =>
                Background = new SolidColorBrush(Colors.Black.WithAlpha(0.3f)));

        // Also animate the content sliding up from below the screen
        this.Animate("popupContainer",
            callback: v => popupContainer.TranslationY = (int)(contentHeight - v),
            start: -contentHeight,
            end: contentHeight,
            length: 500,
            easing: Easing.CubicInOut,
            finished: (v, k) => popupContainer.TranslationY = 0);

        return done.Task;
    }

    private Task PoppingOut()
    {
        Border popupContainer = (Border)GetTemplateChild("popupContainer");

        var done = new TaskCompletionSource();

        // Measure the content size so we know how much to translate
        Size contentSize = popupContainer.Measure(Width, Height);
        double windowHeight = contentSize.Height;

        // Start fading out the background
        this.Animate("Background",
            callback: v => Background = new SolidColorBrush(Colors.Black.WithAlpha((float)v)),
            start: 0.3d,
            end: 0d,
            rate: 32,
            length: 350,
            easing: Easing.CubicIn,
            finished: (v, k) => Background = new SolidColorBrush(Colors.Black.WithAlpha(0.0f)));

        // Start sliding the content down below the bottom of the screen
        this.Animate("popupContainer",
            callback: v => popupContainer.TranslationY = (int)(windowHeight - v),
            start: windowHeight,
            end: -windowHeight,
            length: 500,
            easing: Easing.CubicInOut,
            finished: (v, k) =>
            {
                popupContainer.TranslationY = windowHeight;
                popupContainer.IsVisible = false;
                done.TrySetResult();
            });

        // We return the task so we can wait for the animation to finish
        return done.Task;
    }
}