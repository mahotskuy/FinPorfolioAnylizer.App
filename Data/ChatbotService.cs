using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenAI_API;

public class ChatbotService
{
    private readonly IConfiguration _configuration;

    public ChatbotService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SendMessageAsync(string message)
    {
        OpenAIAPI api = new OpenAIAPI("YOUR_API_KEY");

        return "Some";
    }
}
