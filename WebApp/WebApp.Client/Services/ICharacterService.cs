using WebApp.Client.Models;

namespace WebApp.Client.Services;

public interface ICharacterService
{
    Task<Character> GetCharacterById(string id);
    Task<List<Character>?> GetCharacters();
    Task PostCharacter(Character character);
    Task PutCharacter(Character character);
    Task DeleteCharacter(string id);
}