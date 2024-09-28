using LoanCalculator.Model;
using Microcharts;
using SkiaSharp;
using System.Text.Json;

namespace LoanCalculator.View;

public partial class LoanResultPage : ContentPage
{
    public LoanResultPage(LoanCalculationRequest request)
    {
        InitializeComponent();
        var result = CalculateLoanResults(request);
        BindingContext = result;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private LoanCalculationResult CalculateLoanResults(LoanCalculationRequest request)
    {
        var calculator = new Services.LoanCalculator();
        var debtProgress = calculator.CalculateRemainingDebt(request);

        var maxDebt = debtProgress.Max();

        var entries = debtProgress.Select((value, index) => new ChartEntry((float)(value))
        {
            Label = $"Μ³ρÿφό {index + 1}",
            ValueLabel = $"{value:C}",
            Color = SKColor.Parse("#68B9C0")
        }).ToList();

        var loanCalculationResult = new LoanCalculationResult
        {
            LoanCalculationRequest = request,
            CalculationResult = debtProgress.LastOrDefault(),
            DebtProgressChart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Spline,
                LineSize = 4,
                PointMode = PointMode.None,
                LabelTextSize = 20,
                BackgroundColor = SKColors.White
            }
        };

        SaveData(loanCalculationResult);

        return loanCalculationResult;
    }

    private void SaveData(LoanCalculationResult result)
    {
        var fileName = FileSystem.AppDataDirectory + "/Result.json";
        var json = JsonSerializer.Serialize(result);
        File.WriteAllText(fileName, json);
    }
}