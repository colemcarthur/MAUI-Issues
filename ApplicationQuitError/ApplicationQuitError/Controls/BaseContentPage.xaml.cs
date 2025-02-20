namespace ApplicationQuitError.Controls;

public partial class BaseContentPage : ContentPage
{
    private bool settingPageSizeOptions;

    private double pageWidth;

    public double PageWidth
    {
        get => pageWidth;
        set
        {
            pageWidth = value;
            OnPropertyChanged();
        }
    }

    public static readonly BindableProperty IsPageBusyProperty =
        BindableProperty.Create(
            nameof(IsPageBusy),
            typeof(bool),
            typeof(BaseContentPage),
            false,
            BindingMode.TwoWay);

    public bool IsPageBusy
    {
        get => (bool)GetValue(IsPageBusyProperty);
        set => SetValue(IsPageBusyProperty, value);
    }

    public static readonly BindableProperty HasAcrylicViewProperty =
        BindableProperty.Create(
            nameof(HasAcrylicView),
            typeof(bool),
            typeof(BaseContentPage),
            true,
            BindingMode.TwoWay);

    public bool HasAcrylicView
    {
        get => (bool)GetValue(HasAcrylicViewProperty);
        set => SetValue(HasAcrylicViewProperty, value);
    }

    public static readonly BindableProperty PageHorizontalOptionsProperty =
        BindableProperty.Create(
            nameof(PageHorizontalOptions),
            typeof(LayoutOptions),
            typeof(BaseContentPage),
            LayoutOptions.Start,
            BindingMode.TwoWay);

    public LayoutOptions PageHorizontalOptions
    {
        get => (LayoutOptions)GetValue(PageHorizontalOptionsProperty);
        set => SetValue(PageHorizontalOptionsProperty, value);
    }

    public BaseContentPage()
    {
        InitializeComponent();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        if (settingPageSizeOptions)
            return;

        settingPageSizeOptions = true;

        try
        {
            base.OnSizeAllocated(width, height);

#if WINDOWS || MACCATALYST
            PageWidth = width;
#else
            PageWidth = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) - 60;
#endif
        }
        finally
        {
            settingPageSizeOptions = false;
        }
    }
}