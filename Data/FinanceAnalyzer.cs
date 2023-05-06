using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FinPorfolioAnylizer.Data
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

        public List<Asset> Assets { get; set; } = new();
    }

    public class CustomerInfo
    {
        public int Age { get; set; }
    }

    public enum Region
    {
        Ukraine,
        EuropeUnion,
        UK,
        US,
        Asia
    }

    public record Asset(
        string Name, 
        AssetType AssetType, 
        TermOfAsset TermOfAsset, 
        Currency Currency,
        Region Region, 
        decimal Amount);


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
