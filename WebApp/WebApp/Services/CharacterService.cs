using WebApp.Client.Models;
using WebApp.Client.Services;

namespace WebApp.Services;

public class CharacterService : ICharacterService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public CharacterService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Character> GetCharacterById(string id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("characterApi");
            return await client.GetFromJsonAsync<Character>($"/api/Character/{id}");
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
            var client = _httpClientFactory.CreateClient("characterApi");
            return await client.GetFromJsonAsync<List<Character>>($"/api/Character");
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
            var client = _httpClientFactory.CreateClient("characterApi");
            await client.PutAsJsonAsync("/api/Character", character);
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
            var client = _httpClientFactory.CreateClient("characterApi");
            await client.PutAsJsonAsync($"/api/Character/{character.Id}", character);
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
            var client = _httpClientFactory.CreateClient("characterApi");
            await client.DeleteAsync($"/api/Character/{id}");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}