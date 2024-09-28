using LoanCalculator.Model;

namespace LoanCalculator.Services
{
    public class LoanCalculator
    {
        public List<double> CalculateRemainingDebt(LoanCalculationRequest request)
        {
            double monthlyRate = request.LoanProgram!.InterestRate / 12 / 100;
            double remainingDebt = request.LoanAmount;
            var remainingDebtProgress = new List<double>();

            for (int i = 0; i < request.LoanTerm; i++)
            {
                double interest = remainingDebt * monthlyRate;
                double principalPayment = request.MonthlyPayment - interest;
                remainingDebt -= principalPayment;
                if (remainingDebt < 0) remainingDebt = 0;

                remainingDebtProgress.Add(remainingDebt);
            }

            return remainingDebtProgress;
        }
    }

}
