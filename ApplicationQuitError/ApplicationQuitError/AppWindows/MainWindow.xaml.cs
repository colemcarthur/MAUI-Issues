namespace SCQuote.AppWindows;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		Page = new ContentPage()
		{
			Content = new VerticalStackLayout()
		};
    }
}