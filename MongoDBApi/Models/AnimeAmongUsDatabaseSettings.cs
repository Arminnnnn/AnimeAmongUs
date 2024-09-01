using System;

namespace MongoDBApi.Models;

public class AnimeAmongUsDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;
}
