namespace ApplicationQuitError;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGoToModalClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Modal());
    }
}
