namespace Lab3;

public partial class InfoPage : ContentPage
{
    public InfoPage()
    {
        InitializeComponent();
    }
    private async void MainPageClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}