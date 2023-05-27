using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            var convertedPortfolio = JsonConvert.SerializeObject(portfolio, settings);
            var message = $"I am leaving in Ukraine." +
                $"As a financial consultant analyze my financial assets." +
                $"Analyze financial assets according to the following criteria:" +
                $"- diversification by currency, country, types of assets, types of investments" +
                $"- returns of the portfolio" +
                $"Generate suggestion of portfolio improvements based on investment tools available in Ukraine." +
                $"Згенеруй відповідь українською мовою а не російською." +
                $"My portfolio serialized to json: {convertedPortfolio}";
            return _chatbotService.SendMessageAsync(message);
        }
    }

    public class Portfolio
    {
        public CustomerInfo CustomerInfo { get; set; } = new();

        public List<FinancialGoal> FinancialGoals { get; set; } = new();

        public List<Asset> Assets { get; set; } = new();
    }

    public class CustomerInfo
    {
        public int Age { get; set; }

        public RiskTolerance RiskTolerance { get; set; }

        public MoneyValue AnnualIncome { get; set; }
        public MoneyValue MonthlyExpenses { get; set; }
        public MoneyValue TotalLiabilities { get; set; }
    }

    public class MoneyValue
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

        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }

    public class FinancialGoal
    {
        public string GoalName { get; set; }
        public MoneyValue TargetAmount { get; set; }

        public DateTime? TargetDate { get; set; } = DateTime.UtcNow;
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
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
        public TermOfAsset TermOfAsset { get; set; }
        public Region Region { get; set; }
        public MoneyValue Amount { get; set; }

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
