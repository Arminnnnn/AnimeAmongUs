using MongoDBApi.Authentication;
using MongoDBApi.Models;
using MongoDBApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<AnimeAmongUsDatabaseSettings>(
    builder.Configuration.GetSection("AnimeAmongUsDatabase"));

builder.Services.AddSingleton<CharacterService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.MapControllers();

app.Run();

