using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ChatbotService
{
    private readonly HttpClient _httpClient;

    public ChatbotService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SendMessageAsync(string message)
    {
        var requestBody = new
        {
            prompt = message,
            max_tokens = 50, // Adjust the token limit as needed
            temperature = 0.7 // Adjust the temperature for more or less randomness
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://api.openai.com/v4/engines/gpt-4.0/completions", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return responseObject.choices[0].text.ToString();
        }
        else
        {
            throw new HttpRequestException($"Error: {response.ReasonPhrase}");
        }
    }
}
