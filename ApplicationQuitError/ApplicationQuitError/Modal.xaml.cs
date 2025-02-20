using ApplicationQuitError.Controls;

namespace ApplicationQuitError;

public partial class Modal : BasePopUpPage
{
	public Modal()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await PopModalAsync();
    }

    private void BasePopUpPage_Unfocused(object sender, FocusEventArgs e)
    {

    }
}