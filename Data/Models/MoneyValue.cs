using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FinPortfolioAnalyzer.Data.Models
{
    public record MoneyValue
    {
        public MoneyValue()
        {
            Amount = 0;
            Currency = Currency.USD;
        }

        public MoneyValue(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        [JsonPropertyName("amount")]
        [JsonInclude]
        [Description("The amount of money.")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        [JsonInclude]
        [Description("The currency of the amount.")]
        public Currency Currency { get; set; }
    }
}
