namespace WebApp.Client.Services;

public class CharacterService
{
    private readonly string _baseUrl = "http://localhost:5185";
    
    public async Task<string> GetAllCharacterData()
    {
        HttpClient client = new();

        string data = await GetCharacterDataStringAsync(client);

        return data;
    }
    
    private async Task<string?> GetCharacterDataStringAsync(HttpClient client)
    {
        string requestUrl = $"{_baseUrl}/api/Character";

        var response = await client.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        
        Console.WriteLine($"Error Get: {response.StatusCode}");

        return null;
    }
    
}