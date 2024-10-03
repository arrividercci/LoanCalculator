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
            await DisplayAlert("�������", "���� �����, �������� �� ����", "��");
            return;
        }

        if (string.IsNullOrWhiteSpace(PrincipalEntry.Text) ||
            string.IsNullOrWhiteSpace(MonthlyPaymentEntry.Text) ||
            string.IsNullOrWhiteSpace(TermEntry.Text))
        {
            await DisplayAlert("�������", "���� �����, �������� �� ����", "��");
            return;
        }

        double principal = double.Parse(PrincipalEntry.Text);
        double monthlyPayment = double.Parse(MonthlyPaymentEntry.Text);
        int termMonths = int.Parse(TermEntry.Text);
        if (principal < 0)
        {
            await DisplayAlert("�������", "������ �������� ���� �������", "��");
            return;
        }

        if (monthlyPayment <= 0)
        {
            await DisplayAlert("�������", "������ �������� ���� ���������� ���������", "��");
            return;
        }

        if (termMonths <= 0)
        {
            await DisplayAlert("�������", "������ ��������� ����� �������", "��");
            return;
        }

        if (termMonths > loanProgram.MonthTerm)
        {
            await DisplayAlert("�������", $"�������� �������� {loanProgram.Name} �� ����������� �� ����� ����� {loanProgram.MonthTerm}", "��");
            return;
        }

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