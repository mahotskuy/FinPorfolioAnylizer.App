using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FinPortfolioAnalyzer.Data.Models
{
    public class FinancialGoal
    {
        [JsonPropertyName("goal_name")]
        [JsonInclude]
        [Description("Name of the financial goal.")]
        public string GoalName { get; set; }

        [JsonPropertyName("target_amount")]
        [JsonInclude]
        [Description("Target amount for the financial goal.")]
        public MoneyValue TargetAmount { get; set; }

        [JsonPropertyName("target_date")]
        [JsonInclude]
        [Description("Date by which the financial goal is to be achieved.")]
        public DateTime? TargetDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("priority_level")]
        [JsonInclude]
        [Description("Priority level of the financial goal.")]
        public int PriorityLevel { get; set; }
    }
}
