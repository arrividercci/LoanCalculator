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
        // ������� ���� JSON �� ����������
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
            var message = $"������� ����������:\n" +
                          $"�������� ��������: {previeousCalcalations.LoanCalculationRequest.LoanProgram.Name}\n" +
                          $"���� �������: {previeousCalcalations.LoanCalculationRequest.LoanAmount:C}\n" +
                          $"�������� �������: {previeousCalcalations.LoanCalculationRequest.MonthlyPayment:C}\n" +
                          $"������� �����: {previeousCalcalations.CalculationResult:C}";

            await DisplayAlert("�������� ����������", message, "OK");
        }
        else
        {
            await DisplayAlert("�������", "�������� ���������� �� �������.", "OK");
        }
    }

    private async void OnInfoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }
}