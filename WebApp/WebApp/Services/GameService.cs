using WebApp.Client.Models;
using WebApp.Client.Services;

namespace WebApp.Services;

public class GameService
{
    private readonly ICharacterService _characterService;
    
    private List<Character> _characters = new();
    private static List<string> _connectedPlayers = new();
    private List<string> _imposters = new();
    private const string ImposterCharacterId = "66d753b5ca3c5a2735579554";

    public GameService(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    public void SetConnectedPlayers(List<string> connectedPlayers)
    {
        _connectedPlayers = connectedPlayers;
    }
    
    public List<string> GetImposters()
    {
        Random rnd = new();
        int imposterAmount = CalculateAmountOfImposters(_connectedPlayers.Count());

        for (int i = 0; i < imposterAmount; i++)
        {
            _imposters.Add(_connectedPlayers[rnd.Next(_connectedPlayers.Count)]);
        }
        
        return _imposters;
    }

    private int CalculateAmountOfImposters(int playerAmount)
    {
        if (playerAmount <= 5)
        {
            return 1;
        }

        if (playerAmount <= 9)
        {
            return 2;
        }

        return 3;
    }

    public async Task<Character?> GetImposterCharacter()
    {
        if (_characters.Count == 0)
        { 
            _characters = await _characterService.GetCharacters() ?? throw new InvalidOperationException();
        }

        var imposter = _characters.Find(c => c.Id == ImposterCharacterId);
        return imposter;
    }
    
    public async Task<Character> GetInnocentCharacter()
    {
        Random rnd = new();
        
        if (_characters.Count == 0)
        {
            _characters = await _characterService.GetCharacters() ?? throw new InvalidOperationException();
        }
        
        var innocentCharacter = _characters.Where(c => c.Id != ImposterCharacterId).ToList();
        return innocentCharacter[rnd.Next(_characters.Count-1)];
    }
}