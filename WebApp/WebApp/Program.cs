using WebApp.Client.Models;
using WebApp.Client.Services;
using WebApp.Components;
using WebApp.Hubs;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var characterApiBaseUrl = builder.Configuration["MongoDBApi:BaseUrl"];
var characterApiKey = builder.Configuration["MongoDBApi:ApiKey"];

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();
builder.Services.AddSingleton<ICharacterService, CharacterService>();
builder.Services.AddTransient<GameService>();

builder.Services.AddHttpClient("characterApi", client =>
{
    client.BaseAddress = new Uri(characterApiBaseUrl);
    client.DefaultRequestHeaders.Add("x-api-key", characterApiKey);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebApp.Client._Imports).Assembly);

app.MapHub<GameHub>("/gameHub");
app.MapHub<ConnectedHub>("/connectedHub");

app.MapGet("/api/CharacterService/{id}", async (string id, ICharacterService service) =>
{
    return TypedResults.Ok(await service.GetCharacterById(id));
});

app.MapGet("/api/CharacterService", async (ICharacterService service) =>
{
    return TypedResults.Ok(await service.GetCharacters());
});

app.MapPost("/api/CharacterService", async (Character newCharacter, ICharacterService service) =>
{
    await service.PostCharacter(newCharacter);
});

app.MapPut("/api/CharacterService/{id}", async (Character? character, ICharacterService service) =>
{
    await service.PutCharacter(character);
});

app.MapDelete("/api/CharacterService/{id}", async (string id, ICharacterService service) =>
{
    await service.DeleteCharacter(id);
});

app.Run();