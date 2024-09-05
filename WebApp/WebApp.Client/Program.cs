using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddSingleton<ICharacterService, CharacterProxyService>();

await builder.Build().RunAsync();