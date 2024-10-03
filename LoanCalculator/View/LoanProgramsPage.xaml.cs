using LoanCalculator.Model;
using System.Text.Json;

namespace LoanCalculator.View;

public partial class LoanProgramsPage : ContentPage
{
    public List<LoanProgram> LoanPrograms { get; set; }

    public LoanProgramsPage()
    {
        InitializeComponent();
        LoadLoanPrograms();
    }

    private async void LoadLoanPrograms()
    {
        // Зчитуємо файл JSON із програмами
        using var stream = await FileSystem.OpenAppPackageFileAsync("LoanPrograms.json");
        using StreamReader reader = new StreamReader(stream);
        string json = await reader.ReadToEndAsync();
        LoanPrograms = JsonSerializer.Deserialize<List<LoanProgram>>(json);
        LoanProgramsCollection.ItemsSource = LoanPrograms;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedProgram = e.CurrentSelection[0] as LoanProgram;
        if (selectedProgram != null)
        {
            await Navigation.PushAsync(new LoanInputPage(selectedProgram));
        }
    }

    private async void OnViewPreviousCalculationsClicked(object sender, EventArgs e)
    {
        var fileName = FileSystem.AppDataDirectory + "/Result.json";
        var json = await File.ReadAllTextAsync(fileName);
        var previeousCalcalations = JsonSerializer.Deserialize<LoanCalculationResult>(json);
        if (previeousCalcalations != null)
        {
            var message = $"Останній розрахунок:\n" +
                          $"Кредитна програма: {previeousCalcalations.LoanCalculationRequest.LoanProgram.Name}\n" +
                          $"Сума кредиту: {previeousCalcalations.LoanCalculationRequest.LoanAmount:C}\n" +
                          $"Щомісячна виплата: {previeousCalcalations.LoanCalculationRequest.MonthlyPayment:C}\n" +
                          $"Залишок боргу: {previeousCalcalations.CalculationResult:C}";

            await DisplayAlert("Попередні розрахунки", message, "OK");
        }
        else
        {
            await DisplayAlert("Помилка", "Попередні розрахунки не знайдені.", "OK");
        }
    }

    private async void OnInfoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }
}