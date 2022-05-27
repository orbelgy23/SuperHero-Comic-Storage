using BlazorApp2.Client.Services.SuperHeroService;
using BlazorApp2.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Whenever someone wants to inject the ISuperHeroService
// you want to use of the SuperHeroService Implementation Class - Dependency Injection
builder.Services.AddScoped<ISuperHeroService, SuperHeroService>(); 

await builder.Build().RunAsync();
