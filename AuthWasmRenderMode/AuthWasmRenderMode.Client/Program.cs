using AuthWasmRenderMode.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AuthWasmRenderMode.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddHttpClient<IHttpService, HttpService>();
builder.Services.AddSingleton<UserData>();
builder.Services.AddBlazoredLocalStorageAsSingleton();
await builder.Build().RunAsync();
