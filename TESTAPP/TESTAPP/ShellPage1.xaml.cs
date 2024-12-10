namespace TESTAPP;

public partial class ShellPage1 : ContentPage
{

    public ShellPage1()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(ShellPage2)}");
    }

}
