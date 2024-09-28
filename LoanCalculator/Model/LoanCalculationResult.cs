using Microcharts;
using System.Text.Json.Serialization;

namespace LoanCalculator.Model
{
    public class LoanCalculationResult
    {
        public LoanCalculationRequest LoanCalculationRequest { get; set; } = default!;
        public double CalculationResult { get; set; }
        [JsonIgnore]
        public LineChart DebtProgressChart { get; set; } = default!;
    }

}
