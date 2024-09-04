using System.Text.Json;
using WebApp.Client.Models;

namespace WebApp.Client.Services;

public class GameService
{
    private CharacterService _characterService = new();
    private List<Character> _characters = new();
    
    private static List<string> _connectedPlayers = new();
    private List<string> _imposters = new();
    private string _imposterCharacterId = "66d753b5ca3c5a2735579554";

    public GameService(List<string> connectedPlayers)
    {
        _connectedPlayers = connectedPlayers;
    }

    public List<string> GetImposters()
    {
        Random rnd = new();
        int imposterAmount = this.CalculateAmountOfImposters(_connectedPlayers.Count());

        for (int i = 0; i < imposterAmount; i++)
        {
            _imposters.Add(_connectedPlayers[rnd.Next(_connectedPlayers.Count)]);
        }
        
        return _imposters;
    }
    
    public int CalculateAmountOfImposters(int playerAmount)
    {
        if (playerAmount <= 5)
        {
            return 1;
        }
        else if (playerAmount <= 9)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public async Task<Character?> GetImposterCharacter()
    {
        if (_characters.Count == 0)
        {
            await this.GetCharactersThroughCharacterService();
        }

        var imposter = _characters.FirstOrDefault(c => c.Id == _imposterCharacterId);
        return imposter;
    }
    
    public async Task<Character> GetInnocentCharacter()
    {
        Random rnd = new();
        
        if (_characters.Count == 0)
        {
            await this.GetCharactersThroughCharacterService();
        }
        
        var innocentCharacter = _characters.Where(c => c.Id != _imposterCharacterId).ToList();
        return innocentCharacter[rnd.Next(_characters.Count-1)];
    }

    private async Task GetCharactersThroughCharacterService()
    {
        try
        {
            var charactersResult = await _characterService.GetAllCharacterData();

            var characters = JsonSerializer.Deserialize<List<Character>>(charactersResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _characters = characters ?? throw new NullReferenceException();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
        
    }
}