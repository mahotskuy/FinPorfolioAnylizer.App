using System.ComponentModel;

namespace FinPortfolioAnalyzer.Data.Models
{
    public class AnalyzerPredictionData
    {
        [Description("Predicted Inflation in USA")]
        public float USAInflation { get; set; } = 0.08F;
        [Description("Predicted Inflation in Ukraine")]
        public float UkraineInflation { get; set; } = 0.15F;

        [Description("Predicted deposit deposit interest in Ukraine in UAH currency")]
        public float DepositInUAH { get; set; } = 0.12F;
        [Description("Predicted deposit deposit interest in Ukraine in US currency")]
        public float DepositInUS { get; set; } = 0.01F;
        [Description("Predicted interest on S&P500 index")]
        public float S_And_P { get; set; } = 0.08F;
    }
}
