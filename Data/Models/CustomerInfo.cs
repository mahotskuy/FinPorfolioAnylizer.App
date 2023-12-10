using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FinPortfolioAnalyzer.Data.Models
{
    public class CustomerInfo
    {
        [JsonPropertyName("age")]
        [JsonInclude]
        [Description("Customer's age.")]
        public int Age { get; set; }

        [JsonPropertyName("country")]
        [JsonInclude]
        [Description("Customer's country of residence.")]
        public string Country { get; set; } = "Ukraine";

        [JsonPropertyName("risk_tolerance")]
        [JsonInclude]
        [Description("Customer's risk tolerance level.")]
        public RiskTolerance RiskTolerance { get; set; }

        [JsonPropertyName("annual_income")]
        [JsonInclude]
        [Description("Customer's annual income.")]
        public MoneyValue AnnualIncome { get; set; } = new();

        [JsonPropertyName("monthly_expenses")]
        [JsonInclude]
        [Description("Customer's monthly expenses.")]
        public MoneyValue MonthlyExpenses { get; set; } = new();

        [JsonPropertyName("total_liabilities")]
        [JsonInclude]
        [Description("Customer's total liabilities.")]
        public MoneyValue TotalLiabilities { get; set; } = new();
    }
}
