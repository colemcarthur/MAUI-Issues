namespace TESTAPP;

public partial class ShellPage2 : ContentPage
{

    public ShellPage2()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(ShellPage1)}");
    }

}
