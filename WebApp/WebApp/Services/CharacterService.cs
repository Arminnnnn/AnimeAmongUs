using System.Text.Json;
using WebApp.Client.Models;
using WebApp.Client.Services;

namespace WebApp.Services;

public class CharacterService : ICharacterService
{
    private readonly string _baseUrl = "https://animeamongus.onrender.com";
    
    public async Task<Character> GetCharacterById(string id)
    {
        try
        {
            HttpClient client = new();
            
            string dataResult = await GetCharacterByIdStringAsync(client, id);
            
            var character = JsonSerializer.Deserialize<Character>(dataResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return character ?? throw new NullReferenceException();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
        
        return null;
    }
    
    public async Task<List<Character>?> GetCharacters()
    {
        try
        {
            HttpClient client = new();
            
            string dataResult = await GetCharactersStringAsync(client);
            
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

    public async Task PostCharacter(Character character)
    {
        try
        {
            HttpClient client = new();
            string requestUrl = $"{_baseUrl}/api/Character";

            await client.PostAsJsonAsync(requestUrl, character);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    public async Task PutCharacter(Character character)
    {
        try
        {
            HttpClient client = new();
            string requestUrl = $"{_baseUrl}/api/Character/{character.Id}";

            await client.PutAsJsonAsync(requestUrl, character);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    public async Task DeleteCharacter(string id)
    {
        try
        {
            HttpClient client = new();
            string requestUrl = $"{_baseUrl}/api/Character/{id}";

            await client.DeleteAsync(requestUrl);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private async Task<string> GetCharacterByIdStringAsync(HttpClient client, string id)
    {
        string requestUrl = $"{_baseUrl}/api/Character/{id}";
        
        var response = await client.GetAsync(requestUrl);
        
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        
        Console.WriteLine($"Error Get: {response.StatusCode}");
        return null;
    }
    
    private async Task<string?> GetCharactersStringAsync(HttpClient client)
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