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
}
