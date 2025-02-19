namespace ApplicationQuitError;

public partial class Modal : ContentPage
{
	public Modal()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync(); 
    }
}