using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

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
            Model = Model.ChatGPTTurbo,
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

        string responseMessage;
        try
        {
            var response = await api.Chat.CreateChatCompletionAsync(chatRequest);

            responseMessage = response.ToString();

            var reTryCount = 0;
            while (response.Choices[0].FinishReason != "stop" && reTryCount < 3)
            {
                response = await api.Chat.CreateChatCompletionAsync("continue");
                responseMessage += response.ToString();
                reTryCount++;
            }
        }
        catch (Exception ex)
        {
            responseMessage = $"Exception occurred: {ex.Message}";
        }

        return responseMessage;
    }
}
