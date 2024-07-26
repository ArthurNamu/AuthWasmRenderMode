using AuthWasmRenderMode.Client.Pages;
using AuthWasmRenderMode.Client.Services;
using AuthWasmRenderMode.Components;
using AuthWasmRenderMode.Components.Pages.IAM;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    //.AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

builder.Services.AddAuthentication("TdtsCookie")
    .AddCookie("TdtsCookie");

builder.Services.AddHttpClient<IHttpService, HttpService>();

builder.Services.AddAuthorization();

builder.Services.AddBlazoredLocalStorage();

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
   // .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AuthWasmRenderMode.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapIdentityEndpoints();

app.Run();
