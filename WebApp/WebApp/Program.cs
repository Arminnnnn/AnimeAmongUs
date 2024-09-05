using WebApp.Client.Models;
using WebApp.Client.Services;
using WebApp.Components;
using WebApp.Hubs;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();
builder.Services.AddSingleton<ICharacterService, CharacterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.MapPut("/api/CharacterService/{id}", async (Character character, ICharacterService service) =>
{
    await service.PutCharacter(character);
});

app.MapDelete("/api/CharacterService/{id}", async (string id, ICharacterService service) =>
{
    await service.DeleteCharacter(id);
});

app.Run();