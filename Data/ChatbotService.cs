using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Chat;

public class ChatbotService
{
    private readonly IConfiguration _configuration;

    public ChatbotService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SendMessageAsync(string message)
    {
        OpenAIAPI api = new OpenAIAPI(_configuration["Settings:OpenAI:API_KEY"]);
        var chatRequest = new ChatRequest()
        {
            Temperature = 0,
            Messages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Role = ChatMessageRole.System,
                    Content = "You are financial consultant who what save money from inflation and increase my capital in future"
                },
                new ChatMessage
                {
                    Role = ChatMessageRole.User,
                    Content = message
                }
            }
        };

        var response = await api.Chat.CreateChatCompletionAsync(chatRequest);

        var responseMessage = response.ToString();

        var reTryCount = 0;
        if (response.Choices[0].FinishReason != "stop" && reTryCount < 3)
        {
            response = await api.Chat.CreateChatCompletionAsync("continue");
            responseMessage += response.ToString();
            reTryCount++;
        }
        return responseMessage;
    }
}
