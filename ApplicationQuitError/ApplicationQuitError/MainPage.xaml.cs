using ApplicationQuitError.Controls;

namespace ApplicationQuitError;

public partial class MainPage : BaseContentPage
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
