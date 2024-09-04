using System.Text.Json;
using WebApp.Client.Models;

namespace WebApp.Client.Services;

public class CharacterService
{
    private readonly string _baseUrl = "http://localhost:5185";
    
    public async Task<List<Character>?> GetAllCharacterData()
    {
        try
        {
            HttpClient client = new();
            
            string dataResult = await GetCharacterDataStringAsync(client);
            
            var characters = JsonSerializer.Deserialize<List<Character>>(dataResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return characters ?? throw new NullReferenceException();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
        
        return null;
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