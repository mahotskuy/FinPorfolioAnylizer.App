using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace FinPortfolioAnalyzer.Data
{
    public class FinanceAnalyzer
    {
        private readonly ChatbotService _chatbotService;

        public FinanceAnalyzer(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public Task<string> AnalyzePortfolio(Portfolio portfolio)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }
            };
            var schemaGenerator = new JSchemaGenerator();
            schemaGenerator.GenerationProviders.Add(new StringEnumGenerationProvider());
            var convertedPortfolio = JsonConvert.SerializeObject(portfolio, settings);
            var message = $"I am leaving in {portfolio.CustomerInfo.Country}." +
                $"As a financial consultant analyze my financial assets. \n" +
                $"Analyze financial assets according to the following criteria:\n" +
                $"- diversification by currency, country, types of assets, types of investments\n" +
                $"- returns of the portfolio\\n" +
                $"Don't specify list of assets in portfolio as person knows about it." +
                $"Calculate what financial goals I can achieve if follows current strategy.\n" +
                //$"Calculate what amount of money I will have if continue following my current the strategy and how much money I can have if change the strategy with your improvements.\n" +
                $"Use your prediction on inflation in USA and in {portfolio.CustomerInfo.Country} \n" +
                $"Here is platform prediction {JsonConvert.SerializeObject(new AnalyzerPredictionData(), settings)}" +
                $" and schema with descriptions: {schemaGenerator.Generate(typeof(AnalyzerPredictionData))}" +
                $"Generate suggestion of portfolio improvements based on investment tools available in {portfolio.CustomerInfo.Country}.\n" +
                $"Generate response in language of {portfolio.CustomerInfo.Country}\n but never use Russian language" +
                $"My portfolio serialized to json: {convertedPortfolio}\n" +
                $"The JSON has this schema. Read information about meaning of properties from here: {schemaGenerator.Generate(typeof(Portfolio))}";
            return _chatbotService.SendMessageAsync(message);
        }
    }

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

    public record MoneyValue
    {
        public MoneyValue() {
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

    public enum Region
    {
        Ukraine,
        EuropeUnion,
        UK,
        US,
        Asia
    }

    public enum RiskTolerance
    {
        Low,
        Medium,
        High
    }

    public class Asset
    {
        [JsonPropertyName("name")]
        [JsonInclude]
        [Description("Name of the asset.")]
        public string Name { get; set; }

        [JsonPropertyName("asset_type")]
        [JsonInclude]
        [Description("Type of the asset.")]
        public AssetType AssetType { get; set; }

        [JsonPropertyName("term_of_asset")]
        [JsonInclude]
        [Description("Term of the asset.")]
        public TermOfAsset TermOfAsset { get; set; }

        [JsonPropertyName("region")]
        [JsonInclude]
        [Description("Region where the asset is located.")]
        public Region Region { get; set; }

        [JsonPropertyName("amount")]
        [JsonInclude]
        [Description("Value of the asset.")]
        public MoneyValue Amount { get; set; } = new();

        public Asset(string name, AssetType assetType, TermOfAsset termOfAsset, Region region, MoneyValue amount)
        {
            Name = name;
            AssetType = assetType;
            TermOfAsset = termOfAsset;
            Region = region;
            Amount = amount;
        }
    }
 ;


    public enum Currency
    {
        USD,
        EURO,
        UAH
    }

    public enum TermOfAsset
    {
        Current,
        UpTo3Months,
        From3To12Months,
        From1To3Years,
        MoreThan10Years
    }

    public enum AssetType
    {
        Cash,
        CurrentAccount,
        Deposit,
        Bonds,
        ETF_Long,
        ETF_Short,
        RealEstate
    }
}
