using System.Net.Http.Json;
using WebApp.Client.Models;

namespace WebApp.Client.Services;

public class CharacterProxyService : ICharacterService
{
    private readonly HttpClient _httpClient;
    
    public CharacterProxyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Character> GetCharacterById(string id)
    {
        var character = await _httpClient.GetFromJsonAsync<Character>($"/api/CharacterService/{id}");
        return character;
    }

    public async Task<List<Character>?> GetCharacters()
    {
        var characters = await _httpClient.GetFromJsonAsync<List<Character>>("/api/CharacterService");
        return characters;
    }

    public async Task PostCharacter(Character newCharacter)
    {
        await _httpClient.PostAsJsonAsync($"/api/CharacterService", newCharacter);
    }

    public async Task PutCharacter(Character updatedCharacter)
    {
        await _httpClient.PutAsJsonAsync($"/api/CharacterService/{updatedCharacter.Id}", updatedCharacter);
    }

    public async Task DeleteCharacter(string id)
    {
        await _httpClient.DeleteAsync($"api/CharacterService/{id}");
    }
}