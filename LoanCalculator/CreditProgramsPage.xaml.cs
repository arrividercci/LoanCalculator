namespace LoanCalculator;

public partial class CreditProgramsPage : ContentPage
{
    public CreditProgramsPage()
    {
        InitializeComponent();
    }

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Вибрана програма", $"Ви вибрали {creditProgramsPicker.SelectedItem}", "ОК");
    }
}