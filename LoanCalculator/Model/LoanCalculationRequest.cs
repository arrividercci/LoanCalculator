namespace LoanCalculator.Model
{
    public class LoanCalculationRequest
    {
        public double LoanAmount { get; set; }
        public double MonthlyPayment { get; set; }
        public int LoanTerm { get; set; }
        public LoanProgram? LoanProgram { get; set; }
    }
}
