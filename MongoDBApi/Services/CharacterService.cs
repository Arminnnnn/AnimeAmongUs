using MongoDBApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoDBApi.Services;

public class CharacterService
{
    private readonly IMongoCollection<Character> _characterCollection;

    public CharacterService(
        IOptions<AnimeAmongUsDatabaseSettings> animeAmongUsDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            animeAmongUsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            animeAmongUsDatabaseSettings.Value.DatabaseName);

        _characterCollection = mongoDatabase.GetCollection<Character>(
            animeAmongUsDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<Character>> GetAsync() =>
        await _characterCollection.Find(_ => true).ToListAsync();

    public async Task<Character?> GetAsync(string id) =>
        await _characterCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Character newCharacter) =>
        await _characterCollection.InsertOneAsync(newCharacter);

    public async Task UpdateAsync(string id, Character updatedCharacter) =>
        await _characterCollection.ReplaceOneAsync(x => x.Id == id, updatedCharacter);

    public async Task RemoveAsync(string id) =>
        await _characterCollection.DeleteOneAsync(x => x.Id == id);
}
