using LoanCalculator.Model;

namespace LoanCalculator.View;

public partial class LoanInputPage : ContentPage
{
    private LoanProgram loanProgram;
    public LoanInputPage(LoanProgram loanProgram)
    {
        this.loanProgram = loanProgram;
        InitializeComponent();
        BindingContext = this.loanProgram;
    }

    private async void OnCalculateClicked(object sender, EventArgs e)
    {
        if (loanProgram == null)
        {
            await DisplayAlert("Помилка", "Будь ласка, заповніть всі поля", "ОК");
            return;
        }

        double principal = double.Parse(PrincipalEntry.Text);
        double monthlyPayment = double.Parse(MonthlyPaymentEntry.Text);
        int termMonths = int.Parse(TermEntry.Text);
        var request = new LoanCalculationRequest
        {
            LoanAmount = principal,
            LoanProgram = loanProgram,
            LoanTerm = termMonths,
            MonthlyPayment = monthlyPayment
        };

        await Navigation.PushAsync(new LoanResultPage(request));
    }
}