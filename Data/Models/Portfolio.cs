using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FinPortfolioAnalyzer.Data.Models
{
    public class Portfolio
    {
        [JsonPropertyName("customer_info")]
        [JsonInclude]
        [Description("Information about the customer.")]
        public CustomerInfo CustomerInfo { get; set; } = new();

        [JsonPropertyName("financial_goals")]
        [JsonInclude]
        [Description("List of customer's financial goals.")]
        public List<FinancialGoal> FinancialGoals { get; set; } = new();

        [JsonPropertyName("assets")]
        [JsonInclude]
        [Description("List of customer's assets.")]
        public List<Asset> Assets { get; set; } = new();
    }
}
